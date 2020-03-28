using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbUp.Sample.Models
{
    [Verb("deploy", HelpText = "Deploy & Create Database")]
    public class DeployOptions
    {
        [Option('c', "connection-string", Required = true, HelpText = "Connection string for the database")]
        public string ConnectionString { get; set; }

        [Option('s', "seed-data", Required = true, HelpText = "option to run seed data as part of deployment")]
        public bool SeedData { get; set; }
    }
}
