using CommandLine;
using DbUp.Helpers;
using DbUp.Sample.Models;
using System;
using System.Reflection;

namespace DbUp.Sample
{
    class Program
    {
        static int Main(string[] args)
        {
           return Parser.Default.ParseArguments<DeployOptions, DestroyOptions>(args)
                .MapResult((DeployOptions deployOptions) => DeployDatabase(deployOptions),
                (DestroyOptions destroyOptions) => DestroyDatabase(destroyOptions),
                (errs) => 1);
        }

        static int DeployDatabase(DeployOptions deployOptions)
        {
            var connectionString = deployOptions.ConnectionString;

            EnsureDatabase.For.SqlDatabase(connectionString);

            var ungrader = DeployChanges.To.SqlDatabase(connectionString)
                .WithScriptsAndCodeEmbeddedInAssembly(Assembly.GetExecutingAssembly(),
                (scriptName) => scriptName.StartsWith("DbUp.Sample.Scripts"))
                .WithTransaction()
                .LogToConsole()
                .Build();

            var result = ungrader.PerformUpgrade();

            if(!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Database Created Successfully!");
            Console.ResetColor();

            if (deployOptions.SeedData)
                return SeedDatabase(connectionString);

            return 0;
        }

        static int SeedDatabase(string connectionString)
        {
            var upgrader = DeployChanges.To.SqlDatabase(connectionString)
                .WithScriptsAndCodeEmbeddedInAssembly(Assembly.GetExecutingAssembly(),
                (scriptName) => scriptName.StartsWith("DbUp.Sample.SeedData"))
                .JournalTo(new NullJournal())
                .LogToConsole()
                .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Database Seeded Successfully!");
            Console.ResetColor();
            return 0;
        }

        static int DestroyDatabase(DestroyOptions destroyOptions)
        {
            DropDatabase.For.SqlDatabase(destroyOptions.ConnectionString);
            return 0;
        }
    }
}
