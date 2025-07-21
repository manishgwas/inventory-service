# Project Structure: Inventory Management Service

This document explains the structure and purpose of each major folder and file in the Inventory Management Service project.

---

## Root Directory

- `Inventory Management Service/` — Main source code and configuration for the API and Docker setup.
- `DEPLOYMENT.md` — Step-by-step deployment guide.
- `README.md` — Project overview, features, and quick start.
- `docker-compose.yml` — Docker Compose configuration for API and SQL Server.
- `InventoryManagementService_PRD.md`, `InventoryManagementService_Technical_PRD.md` — Product and technical requirement docs.
- `Tasks/` — Task tracking and progress markdown files.

---

## Inventory Management Service/

- `Controllers/` — ASP.NET Core API controllers:
  - `CategoryController.cs` — Endpoints for categories
  - `ProductController.cs` — Endpoints for products
  - `SkuController.cs` — Endpoints for SKUs
- `Program.cs` — Main entry point, service configuration, middleware, and endpoint registration.
- `InventoryDbContext.cs` — Entity Framework Core DB context, model configuration, and seeding.
- `Product.cs`, `Category.cs`, `SKU.cs` — Entity models for the database.
- `appsettings.json`, `appsettings.Development.json` — Application and environment-specific configuration (logging, connection strings, etc).
- `Dockerfile` — Docker build instructions for the API.
- `Inventory Management Service.csproj` — .NET project file.
- `bin/`, `obj/` — Build output and intermediate files (auto-generated).
- `Properties/launchSettings.json` — Local launch configuration for debugging.

---

## Docker & Database

- `docker-compose.yml` — Defines two services:
  - `inventory-api`: The ASP.NET Core API
  - `db`: SQL Server 2022 container
- `sqlvolume` — Docker-managed volume for SQL Server data persistence.

---

## How to Navigate

- **API code:** `Inventory Management Service/Controllers/`
- **Models:** `Inventory Management Service/Product.cs`, `Category.cs`, `SKU.cs`
- **Configuration:** `appsettings.json`, `docker-compose.yml`, `Dockerfile`
- **Docs:** `README.md`, `DEPLOYMENT.md`, PRD files
- **Build output:** `bin/`, `obj/`

---

## Adding New Features

- Add new controllers to `Controllers/`.
- Add new models/entities to the root of `Inventory Management Service/` and update `InventoryDbContext.cs`.
- Update configuration in `appsettings.json` or `docker-compose.yml` as needed.

---

## Summary

This structure follows best practices for ASP.NET Core and Dockerized microservices, making it easy to extend, maintain, and deploy.
