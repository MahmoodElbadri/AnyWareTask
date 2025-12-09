# 🚀 AnyWare Orders API

A high-performance .NET Web API for managing orders, featuring **Redis Caching** to optimize data retrieval and reduce database load. This project implements the **Cache-Aside Pattern** to ensure data consistency and high availability.

## ✨ Features

- **Full CRUD Operations:** Create, Read, Update, and Delete orders.
- **Redis Caching:**
  - Implements **Cache-Aside Pattern**.
  - Optimized response time (reduced from ~3.5s to ~12ms).
  - Handles cache invalidation on Create, Update, and Delete operations.
- **Architecture:**
  - **Service Layer Pattern:** Decouples business logic from controllers.
  - **DTO Pattern:** Separates domain entities from API contracts.
- **Robust Validation:** Uses **FluentValidation** to ensure data integrity.
- **Mapping:** Uses **AutoMapper** for clean object-to-object mapping.
- **Global Error Handling:** Centralized middleware for managing exceptions.
- **Database:** SQL Server with **EF Core** (Code-First approach & Data Seeding).

## 🛠️ Tech Stack

- **Framework:** .NET 9 Web API
- **Database:** SQL Server (Entity Framework Core)
- **Caching:** Redis (StackExchange.Redis)
- **Object Mapping:** AutoMapper
- **Validation:** FluentValidation
- **Documentation:** Swagger

## 🚀 Getting Started

Follow these steps to get the project running locally.

### Prerequisites
1.  **.NET 9 SDK** installed.
2.  **SQL Server** (LocalDB or Docker).
3.  **Redis Server** running on `localhost:6379`.
    - *Windows:* Use [Memurai](https://www.memurai.com/) or Docker (`docker run -p 6379:6379 -d redis`).

### Installation

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/MahmoodElbadri/AnyWareTask.git](https://github.com/MahmoodElbadri/AnyWareTask.git)
    cd AnyWareTask
    ```

2.  **Configure Database:**
    Check `appsettings.json` and update the `DefaultConnection` string if necessary. The project uses `(localdb)\\mssqllocaldb` by default.

3.  **Apply Migrations & Seed Data:**
    Open the **Package Manager Console** in Visual Studio and run:
    ```powershell
    Update-Database
    ```
    *This will create the database and seed it with initial test data.*

4.  **Run the Application:**
    Press `F5` in Visual Studio or run:
    ```bash
    dotnet run
    ```

5.  **Explore the API:**
    Navigate to `https://localhost:7xxx/swagger` to test the endpoints via Swagger UI.

## 🧪 How to Test Caching

1.  **First Request (Cache Miss):**
    - Call `GET /api/orders/{id}`.
    - The application fetches from SQL DB and saves to Redis.
    - *Response time: ~Normal DB latency.*
2.  **Subsequent Requests (Cache Hit):**
    - Call the same endpoint again.
    - The application fetches instantly from Redis.
    - *Response time: ~Ultra-low latency (<20ms).*
3.  **Cache Invalidation:**
    - Call `PUT` or `DELETE` on that order.
    - The system automatically removes the stale data from Redis.
    - The next `GET` request will fetch fresh data from the DB.
---

*Developed by Mahmood Elbadri as part of a Technical Assessment.*
