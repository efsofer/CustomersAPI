# Customers API

## Project Overview
This is a RESTful API built with **.NET 6** using **Entity Framework Core** and **SQL Server**. The API manages customer data from the `WideWorldImporters` database and supports CRUD operations.

## Getting Started

### **1️ Prerequisites**
Ensure you have the following installed on your system:
- **.NET 6 SDK** ([Download](https://dotnet.microsoft.com/en-us/download))
- **SQL Server** ([Download](https://www.microsoft.com/en-us/sql-server/sql-server-downloads))
- **Postman** (Optional for API testing)

### **2️⃣ Database Setup**
1. Download the sample database: [WideWorldImporters-Full.bak](https://github.com/Microsoft/sql-server-samples/releases/tag/wide-world-importers-v1.0)
2. Restore the database in SQL Server:
   ```sql
   RESTORE DATABASE WideWorldImporters
   FROM DISK = 'C:\path\to\WideWorldImporters-Full.bak'
   WITH MOVE 'WWI_Primary' TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\WideWorldImporters.mdf',
        MOVE 'WWI_Log' TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\WideWorldImporters.ldf',
        REPLACE;
   ```
3. Update the database connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=WideWorldImporters;Trusted_Connection=True;"
   }
   ```

### **3️ Running the Application**
1. **Restore dependencies:**
   ```sh
   dotnet restore
   ```
2. **Build the project:**
   ```sh
   dotnet build
   ```
3. **Run the API:**
   ```sh
   dotnet run
   ```
4. **Open Swagger UI in your browser:**
   ```
   http://localhost:5000/swagger
   ```

##  API Endpoints

### **1️ Customers**
| Method | Endpoint | Description |
|--------|---------|-------------|
| `GET`  | `/api/customers` | Retrieves all customers |
| `GET`  | `/api/customers/{id}` | Retrieves a customer by ID |
| `POST` | `/api/customers` | Creates a new customer |
| `PUT`  | `/api/customers/{id}` | Updates an existing customer |
| `DELETE` | `/api/customers/{id}` | Deletes a customer |

### **2️ Sample `POST` Request**
```json
{
    "customerID": 1502,
  "customerName": "John Doe",
  "billToCustomerID": 1,
  "customerCategoryID": 2,
  "buyingGroupID": null,
  "primaryContactPersonID": 5,
  "alternateContactPersonID": 6,
  "deliveryMethodID": 3,
  "deliveryCityID": 10,
  "postalCityID": 15,
  "creditLimit": 10000.00,
  "accountOpenedDate": "2023-01-01T00:00:00",
  "standardDiscountPercentage": 5.0,
  "isStatementSent": true,
  "isOnCreditHold": false,
  "paymentDays": 30,
  "phoneNumber": "123-456-7890",
  "faxNumber": "123-456-7890",
  "deliveryRun": null,
  "runPosition": null,
  "websiteURL": "https://example.com",
  "deliveryAddressLine1": "123 Main St",
  "deliveryAddressLine2": null,
  "deliveryPostalCode": "12345",
  "coordinates": [34.7818, 32.0853],
  "postalAddressLine1": "123 PO Box",
  "postalAddressLine2": null,
  "postalPostalCode": "54321",
   "lastEditedBy": 1
}
```

## ✅ Testing with Swagger
Swagger UI is available at:
```
http://localhost:5000/swagger
```
It provides an interactive interface to test all API endpoints.

##  Technologies Used
- **C# & .NET 6**
- **Entity Framework Core**
- **SQL Server**
- **Swagger (Swashbuckle)**

##  Notes for Reviewers
- The API supports `geography` fields using **NetTopologySuite**.
- Concurrency handling is implemented to prevent data conflicts.
- Validation is enforced to ensure correct data is stored.


