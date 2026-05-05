# Payment Service API

A mock Payment Service built as part of a mini e-commerce (Amazon-like) system using **ASP.NET Web API**.
This service handles payment processing, and validation.

## Features

* Process payments for orders
* Simulate payment success/failure
* Prevent duplicate successful payments
* Allow retry for failed payments
* Logging using Serilog (file)

---
##  Architecture

This service follows **Controller → Service → Repository** pattern.

Controller → Service → Repository → Database

---                
##  Project Structure

PaymentService.Api/
│
├── Controllers/        # API endpoints
├── Services/           # Business logic
├── Repositories/       # Data access layer
├── Models/             # Database models
├── DTOs/               # Request/Response
├── Data/               # DbContext
├── Enums/              # PaymentStatus
├── Middleware/         # Request Logging 
└── Program.cs

---
##  Tech Stack

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server / LocalDB
* Serilog (Logging)
  
---
##  API Endpoints

### Process Payment

**POST** `/api/payment`

#### Request

{
  "orderId": 101,
  "amount": 500
}

#### Response (Success)

{
  "success": true,
  "message": "Payment successful",
  "transactionId": 1
}

#### Response (Failure)

{
  "success": false,
  "message": "Payment failed",
  "transactionId": 2
}

---
##  Business Logic

### Payment Flow

1. Validate Order ID via Order API
2. Check existing payments:
   * If **Success** → Block
   * If **Pending** → Block
   * If **Failed** → Allow retry
3. Simulate payment:

   * Even amount → Success
   * Odd amount → Failed
4. Save transaction
5. Send notification

---
##  Payment Status

public enum PaymentStatus
{
    Pending = 0,
    Success = 1,
    Failed = 2
}

---
##  Database Schema

### Payments Table

| Column        | Type     | Description           |
| ------------- | -------- | --------------------- |
| TransactionId | int (PK) | Auto-generated ID     |
| OrderId       | int      | Related order         |
| Amount        | decimal  | Payment amount        |
| Status        | int      | PaymentStatus enum    |
| PaymentDate   | datetime | Default UTC timestamp |
| CreatedAt     | datetime | Audit field           |

---
## Logging

Logging is implemented using **Serilog**.

* File logging (`/logs/log-<date>.txt`)
* Structured logs

---
## Testing Scenarios

###  Successful Payment

{ "orderId": 1, "amount": 200 }

###  Failed Payment

{ "orderId": 2, "amount": 201 }

###  Retry Payment

* First attempt fails -> retry allowed
* Success -> blocks further attempts

---
##  Future Improvements

* Add Docker support
* Implement retry mechanism
* Add payment gateway integration
* Introduce event-driven architecture (RabbitMQ)
* Add unit & integration tests

---
##  Key Learnings

* Microservices communication using HTTP
* Clean architecture (Controller-Service-Repository)
* Handling retries & idempotency
* Logging vs persistence design decisions
* Real-world payment flow simulation

---
##  Run the Project

Bash Commands:

dotnet restore
dotnet build
dotnet run

---
##  Author

Developed as part of a mini amazon E-commerce backend system.
