# Reda

This repository holds an implementation for the [test assignment](https://github.com/albumprinter/dotnet-engineer-assignment).

A try to domain driven approach.

## How to run

- Run SqlServer

    ```text
    docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<YourStrong@Passw0rd>" `
    -p 1433:1433 --name sql-dev -h sql-dev `
    -d mcr.microsoft.com/mssql/server:2019-latest
    ```

- Apply database migrations

    ```ps1
    dotnet ef database update --project .\src\Reda.Infrastructure\ -- "Server=127.0.0.1,1433;Database=Reda;User Id=SA;Password=<YourStrong@Passw0rd>"
    ```

- Run the API project

    ```ps1
    dotnet run --project .\src\Reda.Api\
    ```

- Open browser https://localhost:7042/swagger/index.html

## Technologies

- ASP .Net 6
- Sql Server

## Libraries

- MediatR
- EntityFramework
- AutoMapper
- FluentValidation
- xunit
- Moq
