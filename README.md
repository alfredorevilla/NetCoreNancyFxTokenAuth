## Introduction 
Sample NancyFX API with integrated OAuth2 Authentication using IdentityServer4
(C) 2017 Alfredo O. Revilla

## Getting Started
Sample integration with tests. Contains an API, a client and integration tests.

## Running the API
cd src\NancyWebApp\
dotnet restore
dotnet run

## Connecting the API
Import NancyWebAppClient project and use BasicClient class.

## Run the tests
cd \test\NancyWebApp.Tests
dotnet restore
dotnet test
