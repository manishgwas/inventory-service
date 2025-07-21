# Product Requirements Document (PRD)

## Inventory Management Service for E-Cart Admin

### 1. Overview

The Inventory Management Service is an admin-only backend API for managing product categories, products, and SKUs for an e-cart website. It provides endpoints for CRUD operations, manual stock management, and auto-generated API documentation. The service is designed for internal use by admins, with no public-facing UI or integrations.

---

### 2. Goals & Objectives

- Enable admins to efficiently manage categories, products, and SKUs.
- Ensure data integrity and consistency (e.g., products must belong to a category).
- Provide clear, industry-standard error handling and API documentation.
- Support concurrent admin operations.
- Include automated unit and integration tests.

---

### 3. Features

#### 3.1 Category Management

- Create, read, update, and delete categories.
- Deleting a category deletes all associated products and SKUs.
- All products must belong to a category.

#### 3.2 Product Management

- Create, read (single and list), update, and delete products.
- Products are associated with a single category.
- Products can have multiple SKUs (variants).
- Support searching products by name, filtering by category, and pagination (page and pageSize parameters) when listing products.

#### 3.3 SKU Management

- Create, read, update, and delete SKUs for a product.
- SKU attributes: color, size, price, stock.
- All SKUs must belong to one product.
- Manual stock updates only.

#### 3.4 API Documentation

- Auto-generated API documentation (Swagger/OpenAPI).

#### 3.5 Error Handling

- Use industry-standard error response formats (e.g., HTTP status codes, error messages).

#### 3.6 Testing

- Automated unit and integration tests for all endpoints and core logic.

---

### 4. Non-Functional Requirements

- No authentication required.
- No bulk operations, integrations, reports, notifications, or UI.
- Single warehouse/location support.
- Concurrency: Handle multiple admins making changes simultaneously.
- Use industry-standard naming, length, and uniqueness constraints for all entities.
- No localization, compliance, or backup requirements.

---

### 5. Data Model

#### 5.1 Category

- id (unique, auto-generated)
- name (string, required, unique)
- description (string, optional)

#### 5.2 Product

- id (unique, auto-generated)
- name (string, required, unique within category)
- description (string, optional)
- category_id (foreign key, required)

#### 5.3 SKU

- id (unique, auto-generated)
- product_id (foreign key, required)
- color (string, required)
- size (string, required)
- price (decimal, required)
- stock (integer, required)

---

### 6. API Endpoints (Sample)

- `GET /categories` — List all categories
- `GET /categories/{id}` — Get a single category by ID
- `POST /categories` — Create a category
- `PUT /categories/{id}` — Update a category
- `DELETE /categories/{id}` — Delete a category (and all products/SKUs)
- `GET /products` — List all products
- - Supports search by name (`?search=`), filter by category (`?categoryId=`), and pagination (`?page=`, `?pageSize=`)
- `GET /products/{id}` — Get a single product by ID
- `POST /products` — Create a product
- `PUT /products/{id}` — Update a product
- `DELETE /products/{id}` — Delete a product (and all SKUs)
- `GET /products/{productId}/skus` — List SKUs for a product
- `POST /products/{productId}/skus` — Create a SKU
- `PUT /skus/{id}` — Update a SKU
- `DELETE /skus/{id}` — Delete a SKU

---

### 7. Error Handling

- Use standard HTTP status codes (e.g., 400, 404, 409, 500).
- Error responses include a code, message, and details (if applicable).

---

### 8. Documentation

- Auto-generated Swagger/OpenAPI docs available at `/docs` or `/swagger`.

---

### 9. Testing

- Unit tests for all models and business logic.
- Integration tests for all API endpoints.

---

### 10. Out of Scope

- No user authentication or authorization.
- No bulk import/export.
- No reporting, analytics, or notifications.
- No multi-warehouse or supplier integration.

---
