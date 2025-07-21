# Inventory Management Service Deployment Guide

This document describes how to deploy the Inventory Management Service using Docker Compose. It covers prerequisites, repository setup, configuration, and running the service.

---

## Prerequisites

- **Docker**: Install Docker Desktop (Windows/Mac) or Docker Engine (Linux).
- **Git**: Install Git to clone the repository.
- **.NET SDK 8.0+** (optional, only if you want to build or run locally outside Docker).

---

## 1. Clone the Repository

Open a terminal and run:

```
git clone https://github.com/manishgwas/inventory-service.git
cd inventory-service/Inventory\ Management\ Service
```

> Replace the repo URL with your actual repository if different.

---

## 2. Configuration

### Environment Variables

- The default configuration uses `docker-compose.yml` to set environment variables for the API and SQL Server.
- The API connects to the database using the connection string:
  
  `Server=db,1433;Database=InventoryDb;User Id=sa;Password=Your_password123;TrustServerCertificate=True;Encrypt=False;`

- If you need to override any settings, edit `docker-compose.yml` or `appsettings.json` as needed.

---

## 3. Build and Run with Docker Compose

From the `Inventory Management Service` directory, run:

```
docker compose build --no-cache
```

Then start the services:

```
docker compose up -d
```

- This will build the API image and start both the API and SQL Server containers.
- The API will be available at: [http://localhost:5000/swagger](http://localhost:5000/swagger)
- SQL Server will be available on port 1433 (internal Docker network).

---

## 4. Verify the Deployment

- Open your browser and go to [http://localhost:5000/swagger](http://localhost:5000/swagger) to view and test the API endpoints.
- You can also use `curl` or Postman to test endpoints like:
  - `GET /api/Category`
  - `GET /api/Product`
  - `GET /api/products/{productId}/skus`

---

## 5. Stopping and Cleaning Up

To stop the containers:

```
docker compose down
```

To remove all data (including the SQL database volume):

```
docker compose down -v
```

---

## 6. Notes

- The database is seeded with sample data on first run.
- If you change the connection string or environment variables, rebuild the containers.
- For production, update passwords and review security settings.

---

## Troubleshooting

- If the API cannot connect to the database, ensure both containers are running and the connection string uses `Server=db,1433`.
- Check logs with:
  - `docker compose logs inventory-api`
  - `docker compose logs db`
- For Windows/Mac, Docker Compose networking works out of the box. For Linux, ensure Docker network bridge is enabled.

---

## Contact

For issues, open an issue in the repository or contact the maintainer.
