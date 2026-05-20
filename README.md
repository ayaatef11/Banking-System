# Banking System

It is a modular, scalable banking backend application built with **.NET**, designed following **Clean Architecture, CQRS, and  SOLID principles**.

---

##  Project Structure


---

###  `Application/`

- **Abstractions/**: Pipeline behaviors, interfaces for data access and messaging
- **Common/**: Core service interfaces and mappings
- **Dtos/**: Request/response models
- **Features/**: CQRS Commands, Queries, Handlers for Accounts and Transactions
- **Interfaces/**: Utility interfaces (e.g., `IGuidGenerator`)
- **Services/**: Implementations of utility services
- **DependencyInjection.cs**: Registers Application services for DI

---

###  `Domain/`

- **Accounts/**: Account entity and related enums
- **Transactions/**: Transaction entity and related enums
- **Constants/**: Centralized constant values
- **Extensions/**: Helper methods (e.g., Enum extensions)

---

###  `Infrastructure/`

- **Database/**: EF Core DbContext and Migrations
- **Services/**: Concrete service implementations (e.g., `AccountService`, `TransactionService`)
- **Configurations/**: EF Core entity configurations
- **DependencyInjection.cs**: Registers Infrastructure services for DI

---

###  `Shared/`

- **Shared result types and error handling:**
- Result.cs, Error.cs, ErrorType.cs, ValidatorError.cs

---

### 🔹 `Web.Api/`

- **Endpoints/**: Minimal API endpoints (Accounts, Transactions)
- **Extensions/**: ApplicationBuilder and ServiceCollection extensions
- **Mappings/**: API request DTO mappings
- **Middleware/**: Custom middleware (e.g., Logging)
- **Infrastructure/**: Custom API Results
- **Constants/**: Swagger related constants
- **Program.cs**, **DependencyInjection.cs**: API initialization and service setup
- **Dockerfile**: Containerization support
- **HTTP Request Files**: Example requests (`requests.http`)

---


##  CI/CD - `.github/`

GitHub Actions workflows for automating:

- `ci.yml`: Continuous Integration
- `cd.yml`: Continuous Deployment

---

#  Summary

This project follows:

- Clean Architecture 
- Separation of Concerns 
- Test-Driven Development (TDD) 
- SOLID Principles 
