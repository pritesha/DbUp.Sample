# DbUp.Sample
This is a Sample [DbUp](https://dbup.readthedocs.io/en/latest/) Project.

DbUp is a .NET library that helps you to deploy changes to SQL Server databases. It tracks which SQL scripts have been run already, and runs the change scripts that are needed to get your database up to date.

#Steps to run the project : 

1. To run sql server locally install [docker for desktop](https://hub.docker.com/editions/community/docker-ce-desktop-windows)
2. Then run `docker run --restart=always -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=yourStrong(!)Password' -p 1433:1433 --name sql-2017 -d mcr.microsoft.com/mssql/server:2017-latest`
3. Connect to sql server using `sa` user & check if sql server is running.
4. To deploy the database using DbUp follow steps as mentioned below.
5. Build the project from VS & verify the output, it should create new `sample` database with `seed data`.
6. To deploy the database from prompt : 

```
dotnet run deploy --connection-string "Server=localhost;Initial Catalog=sample;User ID=sa;Password='yourStrong(!)Password';Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;" --seed-data
```

7. To Drop the database from prompt : 
```
dotnet run destroy --connection-string "Server=localhost;Initial Catalog=sample;User ID=sa;Password='yourStrong(!)Password';Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;" --force
```
