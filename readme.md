# CoreMonolith

This repository contains a .NET solution for a CoreMonolith application. It demonstrates a modular monolith architecture with clean architecture principles, utilizing ASP.NET Core for the Web API and Keycloak for authentication and authorization.

## Features

* **Modular Monolith:** The solution is structured into distinct modules (projects) for API, Application, Domain, Infrastructure, and SharedKernel, promoting separation of concerns and maintainability.
* **Clean Architecture:**  Each module adheres to clean architecture principles, with clear boundaries between layers and a focus on testability.
* **ASP.NET Core Web API:** The API module exposes RESTful endpoints for interacting with the application.
* **Keycloak Integration:**  Keycloak is used for authentication and authorization, ensuring secure access to the API.
* **Unit Tests:**  The solution includes comprehensive unit tests for the application logic, ensuring code quality and reliability.
* **CI/CD with Azure DevOps:** The repository contains Azure DevOps YAML pipelines for automated build, test, and deployment processes.
* **SonarCloud Analysis:**  SonarCloud is integrated for static code analysis, helping to maintain code quality and identify potential issues.

## Project Structure

* **CoreMonolith.Api:**  Contains the ASP.NET Core Web API project.
* **CoreMonolith.Application:**  Contains the application logic, including use cases, handlers, and DTOs.
* **CoreMonolith.Domain:**  Contains the domain model, including entities, value objects, and domain events.
* **CoreMonolith.Infrastructure:**  Contains infrastructure concerns, such as database access, external service integrations, and repositories.
* **CoreMonolith.SharedKernel:**  Contains shared components and utilities used across the solution.

| Pipeline | Status |
|----------|--------|
|core-monolith-build-sonar | [![Build Status](https://dev.azure.com/zeox115/CoreMonolith/_apis/build/status%2Fcore-monolith-build-sonar?branchName=master&jobName=core_monolith_build_sonar)](https://dev.azure.com/zeox115/CoreMonolith/_build/latest?definitionId=4&branchName=master)|
|core-monolith-release-server | [![Build Status](https://dev.azure.com/zeox115/CoreMonolith/_apis/build/status%2Fcore-monolith-release-server?branchName=master&jobName=core_monolith_release_server)](https://dev.azure.com/zeox115/CoreMonolith/_build/latest?definitionId=5&branchName=master)|