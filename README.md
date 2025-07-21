# Inventory Management Service

A robust ASP.NET Core Web API for managing inventory categories, products, and SKUs, with SQL Server backend and Dockerized deployment.

---

## Features

- Manage Categories, Products, and SKUs
- RESTful API endpoints
- Entity Framework Core with SQL Server
- Docker Compose for easy deployment
- Swagger UI for API documentation and testing
- Seeded database with sample data

---

## Quick Start

### Prerequisites

- Docker (Desktop or Engine)
- Git

### Clone the Repository

```sh
git clone https://github.com/manishgwas/inventory-service.git
cd inventory-service/Inventory\ Management\ Service
```

### Build and Run with Docker Compose

```sh
docker compose build --no-cache
docker compose up -d
```

- API: [http://localhost:5000/swagger](http://localhost:5000/swagger)
- SQL Server: port 1433 (internal Docker network)

---

## API Endpoints

- `GET /api/Category` — List all categories
- `GET /api/Product` — List all products
- `GET /api/products/{productId}/skus` — List SKUs for a product
- `GET /api/skus/{id}` — Get SKU by ID
- `POST /api/products/{productId}/skus` — Create SKU for a product
- `PUT /api/skus/{id}` — Update SKU
- `DELETE /api/skus/{id}` — Delete SKU

See Swagger UI for full documentation and try-it-out.

---

## Configuration

- Environment variables and connection strings are set in `docker-compose.yml`.
- Default DB connection: `Server=db,1433;Database=InventoryDb;User Id=sa;Password=Your_password123;TrustServerCertificate=True;Encrypt=False;`
- To override, edit `docker-compose.yml` or `appsettings.json`.

---

## Development

- To run locally (without Docker):
  1. Ensure SQL Server is running and update `appsettings.json` connection string.
  2. Run with `dotnet run`.

---

## Troubleshooting

- If the API cannot connect to the database, ensure both containers are running and the connection string uses `Server=db,1433`.
- Check logs with:
  - `docker compose logs inventory-api`
  - `docker compose logs db`

---
