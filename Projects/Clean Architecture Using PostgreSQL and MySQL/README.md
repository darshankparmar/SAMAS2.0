dotnet new sln --name clean-architecture-sample-postgresql-mysql

n

dotnet sln add .\Service1.Domain\Service1.Domain.csproj

dotnet new classlib -o Service1.Application

dotnet sln add .\Service1.Application\Service1.Application.csproj

cd Service1.Application

dotnet add reference ..\Service1.Domain\Service1.Domain.csproj

cd ..

dotnet new classlib -o Service1.Infrastructure

dotnet sln add .\Service1.Infrastructure\Service1.Infrastructure.csproj

cd Service1.Infrastructure

dotnet add reference ..\Service1.Domain\Service1.Domain.csproj

dotnet add reference ..\Service1.Application\Service1.Application.csproj

cd ..

dotnet new webapi --name Service1.API

dotnet sln add .\Service1.API\Service1.API.csproj

cd Service1.API

dotnet add reference ..\Service1.Application\Service1.Application.csproj

dotnet add reference ..\Service1.Infrastructure\Service1.Infrastructure.csproj

cd ..

cd Service1.Infrastructure

dotnet add package Microsoft.EntityFrameworkCore

dotnet add package Microsoft.EntityFrameworkCore.Relational

dotnet add package Microsoft.EntityFrameworkCore.Design

dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

dotnet add package Microsoft.EntityFrameworkCore.SqlServer

dotnet add package Pomelo.EntityFrameworkCore.MySql

cd ..

cd Service1.Application

dotnet add package AutoMapper

cd ..

dotnet build

dotnet run --project Service1.API --launch-profile https

