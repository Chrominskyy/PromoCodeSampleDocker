
# PromoCodeSampleDocker

## Overview
PromoCodeSampleDocker is a sample project for managing promotional codes using .NET 8 and Docker. The project follows a clean architecture approach, separating concerns across different layers and using various technologies like ASP.NET Core, Entity Framework Core, Redis, and Swagger for API documentation.

## Project Structure
- **PromoCode.API**: ASP.NET Core Web API for managing promotional codes.
  - API versioning using Asp.Versioning.
  - Swagger for API documentation.
  - Controllers for handling HTTP requests.
  
- **PromoCode.Application**: Business logic and service layer.
  - Services for handling operations related to promotional codes.
  - Interfaces defining service contracts.
  
- **PromoCode.Domain**: Domain models and entities.
  - `PromotionalCode` entity representing the promotional code.
  - Other domain entities and value objects.
  
- **PromoCode.Infrastructure**: Data access and external integrations.
  - EF Core for database operations.
  - Redis for caching.
  - Repositories for data access and persistence.

## Features
- **API Versioning**: Supports multiple versions of the API using Asp.Versioning.
- **Swagger Integration**: Provides interactive API documentation.
- **Entity Framework Core**: ORM for database operations.
- **Redis Caching**: Improves performance with caching.
- **Docker Support**: Includes Dockerfile and docker-compose.yml for containerization and orchestration.

## Setup and Usage
1. **Clone the repository**:
   ```sh
   git clone https://github.com/Chrominskyy/PromoCodeSampleDocker.git
   cd PromoCodeSampleDocker
   ```
2. **Run with Docker**:
   ```sh
   docker-compose up
   ```
   This command builds and runs the application within Docker containers.

3. **API Endpoints**:
   - Access the API documentation via Swagger at `http://localhost:<port>/swagger`.

## Technologies Used
- **.NET 8**
- **ASP.NET Core**
- **Entity Framework Core**
- **Redis**
- **Docker**
- **Swagger (Swashbuckle)**
- **Asp.Versioning**

## Example Controller
The `PromoCodeController` demonstrates typical CRUD operations for promotional codes, with annotations for Swagger documentation and versioning support.

## Contributing
Feel free to fork the repository, make improvements, and submit pull requests. Contributions are welcome!

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

For more details, visit the [PromoCodeSampleDocker GitHub repository](https://github.com/Chrominskyy/PromoCodeSampleDocker).
