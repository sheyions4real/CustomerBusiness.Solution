Customer Service API

A multi-tenant backend service designed for businesses to manage customers, products, services, schedules, sales, authentication, and tenancy.
This API powers customer-facing business operations and integrates with the Vendor Service API for product availability and vendor order requests.

✨ Features
🔹 Multi-Tenancy

Isolates data per tenant (company/organization).

Each tenant can have its own businesses, users, customers, and sales.

🔹 Business Management

Supports multiple business units under each tenant.

Tracks addresses, contact information, operational status, and more.

🔹 Customer Management

Manage customer profiles, contact details, and addresses.

Track status (active/inactive) and historical data.

🔹 Product & Service Catalog

Manage product listings, stock, SKU, pricing.

Manage services, duration, availability, and status.

🔹 Scheduling

Book and track appointments between businesses and customers.

🔹 Sales & Transactions

Create sales records, sale items, and totals.

Integrated with future vendor syncing (via Vendor API).

🔹 Authentication & Authorization

User accounts

Login sessions, access tokens, refresh tokens

Social providers (Google, Facebook)

🔹 Address System

Reusable address table for businesses, customers, and tenants.

🛠️ Tech Stack

.NET 9 / ASP.NET Core Web API

Entity Framework Core 9

SQL Server

EF Core Tools & Migrations

Swagger / OpenAPI

JWT Authentication

Microservices-ready architecture
