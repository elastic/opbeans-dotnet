# opbeans-dotnet
This is an implementation of the Opbeans Elastic APM demo app in .NET as an [ASP.NET Core Web API](https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-2.2) application. It uses the same
database schema as the [Java](https://github.com/elastic/opbeans-java) version.

By default it will use a pre-populated Sqlite database.

To run the application run the following command from the `opbeans` folder:

    dotnet run

By default the application will listen on `localhost:5000`. In case you would like to change this run the following command from the `opbeans` folder:

    dotnet run --urls=http://localhost:3001

## Docker based

If you'd like to run this demo as a docker container please run the below commands:

    docker build --pull -t opbeans-dotnetapp .
    docker run --rm -ti -p 3001:80 opbeans-dotnetapp


## Makefile
