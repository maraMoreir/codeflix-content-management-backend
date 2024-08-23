# Codeflix - Full Cycle

![Em Desenvolvimento](https://img.shields.io/badge/-EM%20DESENVOLVIMENTO-brightgreen)

This repository contains the backend of the Codeflix content management system, a practical project from the Full Cycle course. The system is responsible for managing the video catalog, performing video encoding, managing subscriptions, and authenticating users.

## Índice
- [About the Project](#about-the-project)
- [Architecture](#architecture)
- [Technologies Used](#technologies-used)

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

### Prerequisites
- [Docker](https://www.docker.com/)
- [Node.js](https://nodejs.org/) (for frontend)
- [Go](https://golang.org/) (for video encoder)
