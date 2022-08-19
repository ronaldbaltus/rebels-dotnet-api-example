#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Rebels.ExampleProject.sln"

RUN dotnet build "tests/Rebels.ExampleProject.CQRS.Tests/Rebels.ExampleProject.CQRS.Tests.csproj" -c Debug -o /app/Tests

RUN dotnet test "tests/Rebels.ExampleProject.CQRS.Tests/Rebels.ExampleProject.CQRS.Tests.csproj"

RUN dotnet build "src/Rebels.ExampleProject.Api/Rebels.ExampleProject.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/Rebels.ExampleProject.Api/Rebels.ExampleProject.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rebels.ExampleProject.Api.dll"]