# Technical Product Requirements Document (PRD)

## 1. Overview

The Inventory Management Service is an admin-only backend REST API for managing product categories, products, and SKUs for an e-cart website. It is designed for internal use by admins, with no public-facing UI or integrations. The service supports CRUD operations, manual stock management, and auto-generated API documentation.

---

## 2. Architecture

- **Backend Framework:** ASP.NET Web API (.NET 8)
- **ORM:** Entity Framework Core
- **Database:** SQL Server
- **Containerization:** Docker (docker-compose for app and database)
- **API Documentation:** Swagger/OpenAPI (auto-generated)
- **Testing:** MSTest (unit, integration, functional)
- **Configuration:** appsettings.json for environment-specific settings

---

## 3. Data Model

### 3.1 Category

- `id` (int, PK, auto-generated)
- `name` (string, required, unique)
- `description` (string, optional)

### 3.2 Product

- `id` (int, PK, auto-generated)
- `name` (string, required, unique within category)
- `description` (string, optional)
- `category_id` (int, FK, required)

### 3.3 SKU

- `id` (int, PK, auto-generated)
- `product_id` (int, FK, required)
- `color` (string, required)
- `size` (string, required)
- `price` (decimal, required)
- `stock` (int, required)

---

## 4. API Endpoints

### 4.1 Category Endpoints

- `GET /categories` — List all categories
- `GET /categories/{id}` — Get a single category by ID
- `POST /categories` — Create a category
- `PUT /categories/{id}` — Update a category
- `DELETE /categories/{id}` — Delete a category (and all associated products/SKUs)

### 4.2 Product Endpoints

- `GET /products` — List all products
  - Supports search by name (`?search=`), filter by category (`?categoryId=`), pagination (`?page=`, `?pageSize=`; default 10, max 100)
- `GET /products/{id}` — Get a single product by ID
- `POST /products` — Create a product
- `PUT /products/{id}` — Update a product
- `DELETE /products/{id}` — Delete a product (and all SKUs)

### 4.3 SKU Endpoints

- `GET /products/{productId}/skus` — List SKUs for a product
- `POST /products/{productId}/skus` — Create a SKU
- `PUT /skus/{id}` — Update a SKU
- `DELETE /skus/{id}` — Delete a SKU

---

## 5. Business Logic & Constraints

- All products must belong to a category.
- Deleting a category deletes all associated products and SKUs (cascading delete).
- Product names must be unique within a category.
- All SKUs must belong to a product.
- Only manual stock updates are allowed.
- Stock updates must be atomic and transactional.
- Hard deletion only (no soft deletes).
- No audit logging or stock history required.

---

## 6. Error Handling & Validation

- Use industry-standard HTTP status codes (400, 404, 409, 500, etc.).
- Error responses must include a code, message, and details (if applicable).
- Input validation is handled at the API layer.
- Use industry-standard error formats (e.g., Problem Details for HTTP APIs).

---

## 7. Concurrency & Transactions

- Use optimistic concurrency control (e.g., row versioning or concurrency tokens).
- Use database transactions for stock updates and other critical operations to prevent race conditions.

---

## 8. Testing

- Use MSTest for unit, integration, and functional tests.
- Automated tests must cover all endpoints and core business logic.
- No performance or load testing required.

---

## 9. Deployment & Environment

- Use Docker for both the application and SQL Server database.
- Use docker-compose to orchestrate multi-container setup.
- All environment-specific configuration (e.g., connection strings) must be managed in `appsettings.json`.

---

## 10. Non-Functional Requirements

- No authentication, authorization, or rate limiting.
- No bulk operations, integrations, reports, notifications, or UI.
- Single warehouse/location support.
- No logging, monitoring, or alerting requirements.
- No future-proofing for multi-warehouse or supplier integration.
- Use industry-standard naming conventions for code and API.

---

## 11. Documentation

- Auto-generated Swagger/OpenAPI documentation must be available at `/docs` or `/swagger`.

---

## 12. Out of Scope

- No user authentication or authorization.
- No bulk import/export.
- No reporting, analytics, or notifications.
- No multi-warehouse or supplier integration.

---

## 13. Glossary

- **SKU:** Stock Keeping Unit, a unique identifier for each product variant.
- **CRUD:** Create, Read, Update, Delete operations.
- **PK:** Primary Key
- **FK:** Foreign Key

---

This document is intended to guide the engineering team in the implementation of the Inventory Management Service as per the agreed requirements.
