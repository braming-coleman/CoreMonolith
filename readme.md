# CoreMonolith

| Pipeline                       | Status                                                                                                                                                                                                                  |
| :----------------------------- | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| core-monolith-build-sonar      | [![Build Status](https://dev.azure.com/zeox115/CoreMonolith/_apis/build/status%2Fcore-monolith-build-sonar?branchName=master&jobName=core_monolith_build_sonar)](https://dev.azure.com/zeox115/CoreMonolith/_build/latest?definitionId=4&branchName=master)      |
| core-monolith-release-server   | [![Build Status](https://dev.azure.com/zeox115/CoreMonolith/_apis/build/status%2Fcore-monolith-release-server?branchName=master&jobName=core_monolith_release_server)](https://dev.azure.com/zeox115/CoreMonolith/_build/latest?definitionId=5&branchName=master) |

**CoreMonolith** is a .NET 9 based solution designed to orchestrate file transfers between various download clients on a cloud server and file management software on an on-premise server. The project leverages a **modular monolith** architecture for enhanced maintainability, scalability, and a clear separation of concerns.

## Features

*   **Automated File Orchestration:** Efficiently manages and automates file transfers between different systems, including download clients and media management software.
*   **Modular Monolith Architecture:** Employs a modular monolith approach for better organization, independent development of modules, and a clear separation of concerns, while maintaining the benefits of a single deployment unit.
*   **Modern .NET Technologies:** Built with .NET 9, ASP.NET Core Web API, and Minimal APIs for high performance and a modern development experience.
*   **Clean Architecture:** Adheres to Clean Architecture principles, resulting in a robust, testable, and maintainable codebase that is independent of external frameworks.
*   **CQRS Pattern:** Implements the Command and Query Responsibility Segregation (CQRS) pattern for improved performance, scalability, and simplified domain logic.
*   **Comprehensive Technology Stack:** Utilizes a variety of industry-standard tools and technologies, including:
    *   **Database:** PostgreSQL with Entity Framework Core for data persistence.
    *   **Messaging:** RabbitMQ with MassTransit for asynchronous inter-module communication.
    *   **Caching:** Redis for distributed caching and improved performance.
    *   **Scheduling:** Hangfire for background job processing.
    *   **Logging:** Serilog for structured logging and monitoring.
    *   **API Gateway:** YARP (Yet Another Reverse Proxy) for request routing and centralized API management.
    *   **Frontend:** Blazor Server for a rich, interactive user interface.
    *   **Validation:** FluentValidation for robust input validation.
    *   **Authentication:** Keycloak with OpenID Connect for secure user authentication and authorization.
    *   **Service Orchestration:** .NET Aspire for simplified deployment and orchestration of distributed applications.

## Project Structure

The solution is organized into several modules, each responsible for a specific domain. Each module follows a layered architecture based on Clean Architecture principles:

*   **Core:** Contains shared components, infrastructure, and utilities used across the entire application.
    *   **Core.Application:** Application layer implementing use cases, CQRS handlers, and domain event handling.
    *   **Core.Domain:** Domain layer containing core business logic, entities, value objects, and domain services. Independent of external frameworks.
    *   **Core.Infrastructure:** Infrastructure layer providing implementations for data persistence, messaging, external service integrations, and other infrastructure concerns.
    *   **Core.WebApi:** Web API layer exposing shared API endpoints and common API-related functionality.

*   **Modules:**
    * **DownloadService:** Handles interactions with download clients (peer-to-peer and Newshosting).
        *   **DownloadService.Application:** Application logic for managing download tasks, including communication with download clients.
        *   **DownloadService.Domain:** Domain model specific to download clients, including concepts like download queues, torrents, NZBs, and related entities.
        *   **DownloadService.Infrastructure:** Infrastructure code for interacting with the specific APIs of various download clients.
        *   **DownloadService.Api:** API endpoints for download-related operations, exposed through the API gateway.
    *   **FileService:** Manages communication and integration with file management software (Sonarr, Radarr, Plex).
        *   **FileManagement.Application:** Application logic for file management operations, like importing, renaming, and organizing media files.
        *   **FileManagement.Domain:** Domain model representing the concepts and entities related to file management systems, such as series, movies, episodes, and media files.
        *   **FileManagement.Infrastructure:** Infrastructure layer responsible for communication with the APIs of Sonarr, Radarr, and Plex.
        *   **FileManagement.Api:** API endpoints for file management interactions, exposed through the API gateway.
    *   **UserService:** Manages user accounts, authentication, authorization, and permissions.
        *   **UserService.Application:** Application logic for user management, including user registration, login, profile management, and permission handling.
        *   **UserService.Domain:** Domain model for users, roles, permissions, and related concepts.
        *   **UserService.Infrastructure:** Infrastructure code for interacting with Keycloak and the database for user-related data.
        *   **UserService.Api:** API endpoints for user management, exposed through the API gateway.

## Testing

The project emphasizes comprehensive testing to ensure code quality and stability:

*   **Unit Tests:** xUnit.net is used as the testing framework, NSubstitute for mocking dependencies, and FluentAssertions for writing expressive assertions.
*   **Functional Tests:** (Add details about your functional testing approach and tools - e.g., SpecFlow, Integration tests with TestServer or WebApplicationFactory)

## Acknowledgments

*   Special thanks to [Milan Jovanović](https://www.youtube.com/@MilanJovanovicTech) for his invaluable resources and guidance on .NET development, Clean Architecture, and software design principles.

## License

This project is licensed under the [MIT License](./LICENSE).