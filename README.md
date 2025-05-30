# EDMS Microservice Platform

An in-development Electronic Document Management System (EDMS) built with .NET microservices and clean architecture principles.  
This platform is designed to handle document archiving, workflow automation, and secure storage, aiming to provide scalable and modular solutions for enterprise document management needs.

---

## Project Overview

The EDMS Microservice Platform is a modular system that leverages microservices architecture to manage documents efficiently.  
It focuses on scalability, maintainability, and separation of concerns, ensuring each service handles a specific domain responsibility.

---

## Architecture & Technologies

- **Framework**: .NET 8 with Clean Architecture
- **Microservices**: Independent services for user management, document processing, and workflow orchestration
- **API Gateway**: Routing and load balancing (e.g., Ocelot)
- **Service Discovery**: Dynamic service registration and discovery (e.g., Consul)
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: JWT-based authentication and authorization
- **Containerization**: Docker and Docker Compose for environment consistency
- **CI/CD**: GitHub Actions for automated testing and deployment

## Current Project Structure

```plaintext
EDMS-MicroSservice/
├── services/
│   └── UserService/
│       ├── Application/
│       │   ├── Interfaces/
│       │   ├── DTOs/
│       │   └── Commands/
│       │
│       ├── Domain/
│       │   ├── Entities/
│       │   ├── Interfaces/
│       │   └── Common/
│       │
│       ├── Infrastructure/
│       │   ├── Repositories/
│       │   ├── Migrations/
│       │   ├── Services/
│       │   └── DbContexts/
│       │
│       ├── API/
│       │   ├── Controllers/
│       │   ├── Extensions/
│       │   ├── Middlewares/
│       │   └── Program.cs
│       │
│       ├── Shared/
│       ├── appsettings.json
│       ├── Dockerfile
│       └── UserService.csproj
│
├── docker-compose.yml
├── EDMS Microservice.sln
├── .gitignore
├── LICENSE
└── README.md

```


## Key Features

- **Modular Design**: Each microservice is independently deployable and scalable.
- **Clean Architecture**: Promotes separation of concerns and testability.
- **Scalability**: Designed to handle increasing loads by scaling services horizontally.
- **Security**: Implements JWT for secure authentication and authorization.
- **Containerization**: Ensures consistent environments across development, testing, and production.

---

## Getting Started

### Prerequisites

- .NET 6 SDK
- Docker & Docker Compose
- SQL Server

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/simplexhopee/EDMS-MicroSservice.git

2. Navigate to the project directory:

    cd EDMS-MicroSservice

3. Build and run the services using Docker Compose:

docker-compose up --build

### Roadmap
 Implement DocumentService for handling document uploads and storage

 Develop WorkflowService to manage document approval processes

 Integrate API Gateway for unified access to microservices

 Set up Service Discovery for dynamic service management

 Implement CI/CD pipelines for automated testing and deployment

### Contributing
Contributions are welcome! Please fork the repository and submit a pull request for any enhancements or bug fixes.

### License
This project is licensed under the GPL-3.0 License.

### Contact
For questions or collaboration opportunities, please contact @simplexhopee.
