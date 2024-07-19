dotnet new sln --name clean-architecture-sample

dotnet new classlib -o Member.Domain

dotnet sln add .\Member.Domain\Member.Domain.csproj

dotnet new classlib -o Member.Application

dotnet sln add .\Member.Application\Member.Application.csproj

cd Member.Application

dotnet add reference ..\Member.Domain\Member.Domain.csproj

cd ..

dotnet new classlib -o Member.Infrastructure

dotnet sln add .\Member.Infrastructure\Member.Infrastructure.csproj

cd Member.Infrastructure

dotnet add reference ..\Member.Domain\Member.Domain.csproj

dotnet add reference ..\Member.Application\Member.Application.csproj

cd ..

dotnet new webapi --name Member.API

dotnet sln add .\Member.API\Member.API.csproj

cd Member.API

dotnet add reference ..\Member.Domain\Member.Domain.csproj

dotnet add reference ..\Member.Application\Member.Application.csproj

dotnet add reference ..\Member.Infrastructure\Member.Infrastructure.csproj

cd ..

dotnet build

dotnet run

dotnet run --project Member.API --launch-profile https

