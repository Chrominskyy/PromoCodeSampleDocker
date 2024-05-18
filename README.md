
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

## API Documentation

### PromoCodeController

#### GetPromoCode
**Endpoint:** `GET /api/v1/promocode/{id}`  
**Description:** Retrieves a promotional code by its ID.  
**Parameters:** 
- `id` (Guid): The ID of the promotional code to retrieve.

**Responses:**
- `200 OK`: If the promotional code is found.
- `404 Not Found`: If the promotional code is not found.

#### CreatePromoCode
**Endpoint:** `POST /api/v1/promocode`  
**Description:** Creates a new promotional code.  
**Parameters:** 
- `createPromoCodeDto` (CreatePromoCodeDto): The data for the new promotional code.

**Responses:**
- `201 Created`: If the promotional code is created successfully. Returns the created promotional code.
- `400 Bad Request`: If the request data is invalid.

#### UpdatePromoCode
**Endpoint:** `PUT /api/v1/promocode/{id}`  
**Description:** Updates an existing promotional code by its ID.  
**Parameters:** 
- `id` (Guid): The ID of the promotional code to update.
- `updatePromoCodeDto` (UpdatePromoCodeDto): The updated data for the promotional code.

**Responses:**
- `204 No Content`: If the promotional code is updated successfully.
- `404 Not Found`: If the promotional code is not found.

#### DeletePromoCode
**Endpoint:** `DELETE /api/v1/promocode/{id}`  
**Description:** Deletes a promotional code by its ID.  
**Parameters:** 
- `id` (Guid): The ID of the promotional code to delete.

**Responses:**
- `204 No Content`: If the promotional code is deleted successfully.
- `404 Not Found`: If the promotional code is not found.

### ObjectVersioningController

#### GetVersionedObject
**Endpoint:** `GET /api/v1/objectversioning/{id}`  
**Description:** Retrieves a versioned object by its ID.  
**Parameters:** 
- `id` (Guid): The ID of the versioned object to retrieve.

**Responses:**
- `200 OK`: If the versioned object is found.
- `404 Not Found`: If the versioned object is not found.

#### CreateVersionedObject
**Endpoint:** `POST /api/v1/objectversioning`  
**Description:** Creates a new versioned object.  
**Parameters:** 
- `createVersionedObjectDto` (CreateVersionedObjectDto): The data for the new versioned object.

**Responses:**
- `201 Created`: If the versioned object is created successfully. Returns the created versioned object.
- `400 Bad Request`: If the request data is invalid.

#### UpdateVersionedObject
**Endpoint:** `PUT /api/v1/objectversioning/{id}`  
**Description:** Updates an existing versioned object by its ID.  
**Parameters:** 
- `id` (Guid): The ID of the versioned object to update.
- `updateVersionedObjectDto` (UpdateVersionedObjectDto): The updated data for the versioned object.

**Responses:**
- `204 No Content`: If the versioned object is updated successfully.
- `404 Not Found`: If the versioned object is not found.

#### DeleteVersionedObject
**Endpoint:** `DELETE /api/v1/objectversioning/{id}`  
**Description:** Deletes a versioned object by its ID.  
**Parameters:** 
- `id` (Guid): The ID of the versioned object to delete.

**Responses:**
- `204 No Content`: If the versioned object is deleted successfully.
- `404 Not Found`: If the versioned object is not found.


## Contributing
Feel free to fork the repository, make improvements, and submit pull requests. Contributions are welcome!

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

For more details, visit the [PromoCodeSampleDocker GitHub repository](https://github.com/Chrominskyy/PromoCodeSampleDocker).
