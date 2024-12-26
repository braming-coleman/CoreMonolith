# CoreMonolith

| Pipeline | Status |
|----------|--------|
|core-monolith-build-sonar | [![Build Status](https://dev.azure.com/zeox115/CoreMonolith/_apis/build/status%2Fcore-monolith-build-sonar?branchName=master&jobName=core_monolith_build_sonar)](https://dev.azure.com/zeox115/CoreMonolith/_build/latest?definitionId=4&branchName=master)|
|core-monolith-release-server | [![Build Status](https://dev.azure.com/zeox115/CoreMonolith/_apis/build/status%2Fcore-monolith-release-server?branchName=master&jobName=core_monolith_release_server)](https://dev.azure.com/zeox115/CoreMonolith/_build/latest?definitionId=5&branchName=master)|

CoreMonolith is a .NET 9 solution designed to orchestrate file transfers between various download clients residing on a cloud server and file management software on a local server. This project leverages a modular monolith architecture for enhanced maintainability and scalability.

## Features

*   **File Orchestration:** Efficiently manages and automates file transfers between different systems.
*   **Modular Design:** Employs a modular monolith approach for better organization and separation of concerns.
*   **Modern Technologies:** Built with .NET 9, ASP.NET Core Web API, and Minimal APIs.
*   **Clean Architecture:** Adheres to Clean Architecture principles for a robust and testable codebase.
*   **CQRS Pattern:** Implements Command Query Responsibility Segregation (CQRS) for improved performance and scalability.
*   **Comprehensive Tooling:** Utilizes a variety of tools and technologies including:
    *   **Database:** PostgreSQL with Entity Framework Core
    *   **Messaging:** RabbitMQ with MassTransit
    *   **Caching:** Redis
    *   **Scheduling:** Hangfire
    *   **Logging:** Serilog
    *   **API Gateway:** YARP
    *   **Frontend:** Blazor Server Side Web App
    *   **Validation:** FluentValidation
    *   **Authentication:** Keycloak with OpenIdConnect
    *   **Service Orchestration:** Aspire

## Project Structure

The solution is organized into several modules, each responsible for a specific domain:

*   **Core:** Contains shared components and infrastructure.
    *   **Core.Application:**  Application layer with use cases, CQRS handlers, and domain events.
    *   **Core.Domain:**  Domain layer with entities, value objects, and domain logic.
    *   **Core.Infrastructure:** Infrastructure layer for persistence, messaging, and other services.
    *   **Core.WebApi:**  Web API layer for exposing endpoints.

*   **Downloads:** Handles interactions with download clients.
    *   **Downloads.Application:** Application logic for managing downloads.
    *   **Downloads.Domain:** Domain model for download clients and related entities.
    *   **Downloads.Infrastructure:** Infrastructure for interacting with download clients.
    *   **Downloads.WebApi:**  Web API for download-related operations.

*   **FileManagement:** Manages communication with file management software.
    *   **FileManagement.Application:** Application logic for file management operations.
    *   **FileManagement.Domain:** Domain model for file management systems.
    *   **FileManagement.Infrastructure:** Infrastructure for communicating with file management software.
    *   **FileManagement.WebApi:** Web API for file management interactions.

*   **... (other modules)**

## Testing

The project includes comprehensive testing:

*   **Unit Tests:** XUnit, NSubstitute, and FluentValidation are used for unit testing.
*   **Functional Tests:** (Add details about your functional testing approach)

## Acknowledgments

*   Special thanks to [Milan Jovanović](https://www.youtube.com/@MilanJovanovicTech) for his valuable resources and guidance on .NET development and architecture.

## License

This project is licensed under the [MIT License](LICENSE).