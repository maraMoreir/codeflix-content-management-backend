# Codeflix - Full Cycle

![Em Desenvolvimento](https://img.shields.io/badge/-EM%20DESENVOLVIMENTO-brightgreen)

This repository contains the backend of the Codeflix content management system, a practical project from the Full Cycle course. The system is responsible for managing the video catalog, performing video encoding, managing subscriptions, and authenticating users.

## Índice
- [About the Project](#about-the-project)
- [Architecture](#architecture)
- [Technologies Used](#technologies-used)
- [Tools, Patterns and Libs](#tools-patterns-and-libs)

## About the Project
**Codeflix Content Management Backend** is a backend application that supports the management of videos, categories, genres and subscriptions on the Codeflix platform. The system is composed of multiple microservices integrated asynchronously to guarantee scalability and performance.

### Main Features
- Video catalog management (categories, genres, etc.).
- Video encoding for optimized formats (mpeg-dash).
- User authentication via Keycloak.
- Management of subscriptions and plans.
- Search and browse videos using Elasticsearch.

## Architecture
The project architecture is based on decoupled microservices, which communicate synchronously and asynchronously. We use messaging patterns with RabbitMQ and Kafka, as well as integration with cloud storage buckets.

### Main Components:
- **Frontend (Admin and Subscribers)**: React interface for the administrator to manage the video catalog and for subscribers to browse and watch videos.
- **Backend**: Service responsible for the business logic of the video catalog.
- **Video Encoder**: Service dedicated to video processing and encoding.
- **Database**: Uses MySQL to manage catalog relational data and PostgreSQL for subscription data.
- **Messaging**: RabbitMQ and Kafka for asynchronous communication between services.

## Technologies Used
- **Languages**: Go, TypeScript (React), C# (.NET)
- **Databases**: MySQL, PostgreSQL, Elasticsearch
- **Messaging**: RabbitMQ, Apache Kafka, Kafka Connect
- **Authentication**: Keycloak
- **Cloud**: Google Cloud Platform (GCP) for video storage

  
## Tools, Patterns and Libs
This project uses a series of tools, standards and libraries to ensure quality, scalability and resilience, including:

- **Clean Architecture**: We apply Clean Architecture principles to ensure that the code is modular, easy to maintain and scalable.
- **DDD (Domain-Driven Design)**: DDD patterns are used to organize code based on business rules.
- **MediatR**: Implementation of the mediation pattern to decouple requests from commands and events within the application.
- **CI/CD with GitHub Actions**: Integration and continuous delivery using GitHub Actions.
- **MySQL**: Used as the relational database for the video catalog.
- **FluentValidator**: Fluent validation tool to ensure data consistency.
- **Docker**: Used for containerization and orchestration of environments.
- **Migrations with Entity Framework Core**: Database version control using Entity Framework Core migrations.
- **Resilience and Failures with Polly**: Fault management and resilience in the application using the Polly library.
- **GCP Cloud Storage**: Video storage in the cloud with Google Cloud Platform.
- **Rich Listings**: Implementations to improve the data listing experience in the system.
- **Unit of Work**: Implemented to guarantee the consistency of transactions in the database.
- **Messaging with RabbitMQ**: Used for asynchronous communication between microservices.
- **JWT and Roles Authentication**: Implementation of JWT-based authentication and role-based access control (Roles).
- **ASP.NET MVC Filters**: Custom filters in ASP.NET MVC for handling exceptions, logs, among others.
  
 **And many other tools and standards that contribute to the robustness and flexibility of the application.** 

### Prerequisites
- [Docker](https://www.docker.com/)
- [C# .NET](https://dotnet.microsoft.com/) (for backend)
- [Node.js](https://nodejs.org/) (for frontend)
- [Go](https://golang.org/) (for video encoder)
