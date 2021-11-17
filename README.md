
# The Real State Company

A custom implementation of the Clean Architecture Principles with
 .NET 5. Use cases as central organizing structure, 
 decoupled from frameworks and technology details. 
 Built by small components that are developed and tested 
 in isolation.

 


## Badges


[![languages](https://img.shields.io/github/languages/count/droa-dev/TheRealStateCompany)]()
![GitHub last commit](https://img.shields.io/github/last-commit/droa-dev/TheRealStateCompany)
![GitHub branch checks state](https://img.shields.io/github/checks-status/droa-dev/TheRealStateCompany/master)
![GitHub](https://img.shields.io/github/license/droa-dev/TheRealStateCompany)
## Specification

A web API application developed in [.NET 5](https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-5),
created to manage the information necessary for the management of real estate properties.
The solutions implements docker containers for linux for easy deployment.
For security validation, the solution implements [JWT](https://datatracker.ietf.org/doc/html/rfc7519) bearer tokens.
in this initial phase the use of a database has been implemented with [SQL Server](https://www.microsoft.com/en-us/sql-server/developer-get-started) as primery persistence provider in infrastructure, however, the design of the application allows to implement any other
persistence provider easily, like Redis Cache, CosmosDb, etc.
Unit cases were made with [nUnit](https://nunit.org/).

## Features

- Create owners
- Create properties
- Update properties
- Add property images
- Get properties with filters


## Tech Stack

**Server:** .NET 5, Entity Framework Core, JWT

**Testing and UI:** Swagger, nUnit


## API Reference

The entire API reference can be found in the Swagger Client when debugging the project.

![swagger01](https://user-images.githubusercontent.com/20091885/142134544-84c0a396-b55b-4bc8-b598-4da04ab743c0.jpg)

![swagger02](https://user-images.githubusercontent.com/20091885/142134548-ae2cda98-68c7-47f4-8ed0-773dd937129a.jpg)


## Installation

In order to debug correctlythe solution, it is necessary to have the following resources installed:

- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [.NET 5](https://dotnet.microsoft.com/download/dotnet/5.0)

    
## Run Locally

Clone the project

```bash
  git clone https://github.com/droa-dev/TheRealStateCompany.git
```




## Running Tests

To run tests, use a CMD command window, and change directory to the root of the solution.
Run the following command

```bash
  dotnet test
```


## License

[Apache License 2.0](https://github.com/droa-dev/TheRealStateCompany/blob/master/LICENSE)

