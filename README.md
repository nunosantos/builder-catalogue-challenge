# builder-catalogue-challenge

Buildable Sets Application

Table of Contents
* Overview
* Features
* Architecture
* Getting Started
* Prerequisites
* Installation
* Building the Application
* Running the Application
* Main Logic
* API Documentation
* Testing
* Contributing
* License

## Overview

The Buildable Sets Application is a .NET-based web API designed to manage and determine which sets a user can build based on their current collection of pieces. Each set comprises various pieces, and users possess collections containing different variants of these pieces. The application assesses the user’s inventory to identify all possible sets they can construct.

Features
* User Management: Manage user profiles and their collections of pieces.
* Set Management: Define and manage sets composed of various pieces.
* Buildability Assessment: Determine which sets a user can build based on their current collection.
* API Endpoints: Expose RESTful endpoints for interacting with users and sets.
* Comprehensive Testing: Ensure reliability through unit and integration tests.
* API Documentation: Utilize Swagger for interactive API documentation.

## Architecture

The application follows a Clean Architecture approach, ensuring a clear separation of concerns and maintainability. Key layers include:
* Domain Layer: Contains the core business entities such as User, Set, Piece, Part, etc.
* Application Layer: Implements the business logic and services like BuildableSetService.
* Infrastructure Layer: Handles data access, repositories, and external service integrations.
* API Layer: Exposes the functionality through HTTP endpoints using ASP.NET Core.

## Getting Started

### Prerequisites
* .NET 6 SDK or later: Ensure you have the .NET SDK installed. You can download it from here.
* IDE: Visual Studio 2022 or later, Visual Studio Code, or any preferred C# compatible IDE.
* Git: For version control and cloning the repository.

### Installation
1.	Clone the Repository:

```bash
git clone https://github.com/nunosantos/buildable-sets-app.git
cd buildable-sets-app
```

2.	Navigate to the Solution Directory:

`cd BuildableSetsApp`

### Building the Application
1.	Restore Dependencies:

`dotnet restore`

2.	Build the Solution:

`dotnet build`

### Running the Application
1.	Navigate to the API Project:

`cd BuildableSetsApp.Api`


2.	Run the Application:

`dotnet run`

	3.	Access the API:
Once running, navigate to https://localhost:5208/swagger in your browser to access the Swagger UI for interactive API documentation.

## Main Logic

The core logic of the application resides in the Application Layer, specifically within the BuildableSetService. This service is responsible for:
1.	Fetching All Sets:
* Retrieves a summary of all available sets using ISetService.GetAllSets().
2.	Fetching Detailed Set Information:
* For each set, it calls ISetService.GetSetDetails(setID) to obtain detailed information, including the list of required pieces.
3.	Assessing Buildability:
* Evaluates whether the user has sufficient pieces in their collection to build each set using the CanUserBuildSet method.
4.	Mapping to DTOs:
* Maps buildable sets to BuildableSet DTOs containing only the set names for streamlined API responses.

## Key Components:
* BuildableSetService.cs: Contains the business logic for determining buildable sets.
* BuildableSet.cs: A DTO representing the set name to be returned by the API.
* ISetService.cs: Interface for set-related operations, including fetching all sets and set details.
* User.cs, Set.cs, Piece.cs, Part.cs: Domain entities representing the core data structures.

## API Documentation

The application utilizes Swagger to provide interactive API documentation. Once the application is running, access the Swagger UI at:

https://localhost:5028/swagger

## Key Endpoint
* Get Buildable Sets for a User
* Endpoint: GET /buildable-sets/{username}
* Description: Retrieves a list of set names that the specified user can build based on their current collection.
* Response:
* 200 OK: Returns a list of BuildableSet DTOs containing set names.
* 400 Bad Request: If the username is missing or invalid.
* 404 Not Found: If no user is found with the provided username.

## Testing

The application includes both unit tests and integration tests to ensure functionality and reliability.

## Unit Tests
* Project: BuildableSetsApp.Tests
* Framework: xUnit
* Mocking: Utilizes Moq for mocking dependencies.
* Key Tests:
* BuildableSetServiceTests.cs: Tests the logic determining buildable sets.
* Example Test: CanBuildMultipleSets_ReturnsTrueForAllApplicableSets ensures that only sets for which the user has sufficient pieces are returned.

## Integration Tests
* Project: BuildableSetsApp.IntegrationTests
* Framework: xUnit with WebApplicationFactory for in-memory testing.
* Key Tests:
* GetBuildableSetsByUserTests.cs: Tests the API endpoint /buildable-sets/{username} to verify correct responses.

## Running Tests
1.	Navigate to the Test Project Directory:

`cd BuildableSetsApp.Tests`


2.	Run Tests:

`dotnet test`

Hope you have enjoyed it :) 