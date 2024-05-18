
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
API version 1.0 summary description

## Version: PromotionalCode API - 1.0
### /api/v1/ObjectVersioning


#### CreateVersionedObject
**Endpoint:** `POST /api/v1/objectversioning`  
**Description:** Creates a new versioned object.  
**Parameters:** 
- `objectVersioning` ([objectVersioning](PromoCode.Domain/Models/ObjectVersioning.cs): The data for the new versioned object.

**Responses:**
- `201 Created`: If the versioned object is created successfully. Returns the created versioned object.
- `400 Bad Request`: If the request data is invalid.

**Sample request**

    {
	    "id": "73e22ea2-bf8a-449b-ab39-9baa60e47b10",
	    "objectType": "promotionalCode",
	    "objectId": "51be957c-f543-4f4d-b5f8-05bd88ae4328",
	    "objectTenant": "0fb61230-6ab0-4af9-aaa6-3bd18ecfdcf5",
	    "beforeValue": null,
	    "afterValue": "{\"Name\":\"TestCode\",\"Code\":\"Code1234\",\"RemainingUses\":255,\"MaxUses\":255,\"Status\":\"Active\",\"Id\":\"51be957c-f543-4f4d-b5f8-05bd88ae4328\",\"TenantId\":\"0fb61230-6ab0-4af9-aaa6-3bd18ecfdcf5\",\"CreatedAt\":\"2024-05-16T19:34:26.633116Z\",\"UpdatedAt\":null,\"CreatedBy\":\"User\",\"UpdatedBy\":null,\"IsDeleted\":false}",
	    "updatedOn": "2024-05-16T19:34:27.46472",
	    "updatedBy": "User"
    }

#### GetAllVersionedObjects
**Endpoint:** `GET /api/v1/objectversioning`  
**Description:** Retrieves a all versioned objects.  
**Parameters:**


**Responses:**
`200 OK`: If the versioned objects are found.

### /api/v1/ObjectVersioning/{objectId}

#### GetVersionedObject
**Endpoint:** `GET /api/v1/objectversioning/{objectId}`  
**Description:** Retrieves a versioned object by its ID.  
**Parameters:** 
| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| `objectId` | path | ID of the object. | Yes | string (uuid) |
| `objectType` | query | Type of the object. | No | string |
| `objectTenant` | query | Tenant ID of the object. | No | string (uuid) |

**Responses:**
- `200 OK`: If the versioned object is found.
- `404 Not Found`: If the versioned object is not found.

### /api/v1/PromotionalCode

#### GetPromotionalCodes

**Endpoint:**  `GET /api/v1/promotionalcode`  
**Description:** Retrieves all active promotional codes. 
**Parameters:**  

**Responses:**  
-  `200 OK`: If the promotional code is found. 

#### CreatePromotionalCode

**Endpoint:**  `POST /api/v1/promotionalcode`  
**Description:** Creates a new promotional code. 
**Parameters:**  
-  `promotionalCodeDto` ([promotionalCodeDto](PromoCode.Domain/Models/PromotionalCodeDto.cs)): The data for the new promotional code. 

**Responses:**  
-  `201 Created`: If the promotional code is created successfully. Returns the created promotional code. 
-  `400 Bad Request`: If the request data is invalid.

**Sample request**

    {
	    "id":  "3fa85f64-5717-4562-b3fc-2c963f66afa6",
	    "tenantId":  "3fa85f64-5717-4562-b3fc-2c963f66afa6",
	    "createdBy":  "User1",
	    "isDeleted":  false,
	    "name":  "CodeName",
	    "code":  "CodeValue",
	    "remainingUses":  255,
	    "maxUses":  255,
	    "status":  "Active"
    }

#### UpdatePromotionalCode

**Endpoint:**  `PUT /api/v1/promotionalcode/{id}`  
**Description:** Updates an existing promotional code by its ID. Updates only fields that are not null in request. 
**Parameters:**  
-  `id` (Guid): The ID of the promotional code to update. 
-  `promotionalCodeDto` ([promotionalCodeDto](PromoCode.Domain/Models/PromotionalCodeDto.cs)): The updated data for the promotional code. 

**Responses:**  
-  `204 No Content`: If the promotional code is updated successfully. 
-  `404 Not Found`: If the promotional code is not found.

**Sample request**
	

    {
   		"id":  "c89d2a21-9725-4edc-9b1d-dbc02aacffd7",
   		"tenantId":  "0fb61230-6ab0-4af9-aaa6-3bd18ecfdcf5",
   		"updatedBy":  "User2",
   		"name":  "UpdatedName"
   	}
		

### /api/v1/PromotionalCode/{id}

#### GetPromotionalCode

**Endpoint:**  `GET /api/v1/promotionalcode/{id}`  
**Description:** Retrieves a promotional code by its ID. 
**Parameters:**  
-  `id` (Guid): The ID of the promotional code to retrieve. 

**Responses:**  
-  `200 OK`: If the promotional code is found. 
-  `404 Not Found`: If the promotional code is not found.

#### DeletePromotionalCode

**Endpoint:**  `DELETE /api/v1/promotionalcode/{id}`  
**Description:** Deletes a promotional code by its ID. 
**Parameters:**  
-  `id` (Guid): The ID of the promotional code to delete. 

**Responses:**  
-  `204 No Content`: If the promotional code is deleted successfully. 
-  `404 Not Found`: If the promotional code is not found.

### /api/v1/PromotionalCode/{id}/deactivate

#### DeactivatePromotionalCode

**Endpoint:**  `PATCH /api/v1/promotionalcode/{id}`  
**Description:** Deactivates a promotional code by its ID. 
**Parameters:**  
-  `id` (Guid): The ID of the promotional code to deactivate. 

**Responses:**  
-  `204 No Content`: If the promotional code is deactivated successfully. 
-  `404 Not Found`: If the promotional code is not found.

### /api/v1/PromotionalCode/{code}/redeem

#### RedeemPromotionalCode

**Endpoint:**  `GET /api/v1/promotionalcode/{code}/redeem`  
**Description:** Redeems a promotional code by its ID. 
**Parameters:**  
-  `code` (string): The code of the promotional code to redeem. 

**Responses:**  
-  `200 OK`: If the promotional code is found. 
-  `404 Not Found`: If the promotional code is not found.


## Contributing
Feel free to fork the repository, make improvements, and submit pull requests. Contributions are welcome!

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

For more details, visit the [PromoCodeSampleDocker GitHub repository](https://github.com/Chrominskyy/PromoCodeSampleDocker).
