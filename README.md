# 🔧 AutoTallerManager

Backend RESTful para gestión de talleres automotrices — **ASP.NET Core 8**, **PostgreSQL**, **Arquitectura Hexagonal**, **CQRS + MediatR**, **Mapster**.

## 🏗️ Estructura (4 carpetas)

```
AutoTallerManager/
├── Api/                      → Controllers, DTOs, Mappings (Mapster), Services/Auth
├── Application/              → UseCases (CQRS), Abstractions, ValidationBehavior
├── Domain/                   → Entities, ValueObjects, Enums
└── Infrastructure/           → EF Core + PostgreSQL, Repositories, UnitOfWork
```

## 🚀 Inicio Rápido

### Prerrequisitos
- .NET 8 SDK
- PostgreSQL 15+

### 1. Configurar conexión
Edita `Api/appsettings.json`:
```json
"DefaultConnection": "Host=localhost;Port=5432;Database=AutoTallerDB;Username=postgres;Password=TuPassword"
```

### 2. Migración y arranque
```bash
cd Api
dotnet restore
dotnet ef migrations add InitialCreate --project ../Infrastructure
dotnet ef database update --project ../Infrastructure
dotnet run
```

Swagger disponible en `https://localhost:5001`

## 🔑 Auth
```http
POST /api/auth/login  →  { "correo": "...", "password": "..." }
POST /api/auth/register
```
Usar token como `Authorization: Bearer <token>`

## 📋 Endpoints
| Método | Ruta                           | Rol           |
|--------|--------------------------------|---------------|
| POST   | /api/auth/login                | Público       |
| GET    | /api/clientes                  | Autenticado   |
| POST   | /api/ordenes                   | Autenticado   |
| POST   | /api/ordenes/{id}/cerrar       | Mecánico/Admin|
| POST   | /api/repuestos                 | Admin         |
| PATCH  | /api/repuestos/{id}/stock      | Admin         |
| PATCH  | /api/facturas/{id}/pagar       | Admin         |

## ⚙️ Tecnologías
- ASP.NET Core 8 · PostgreSQL · EF Core 8
- MediatR 12 · FluentValidation 11
- **Mapster** (mapping) · BCrypt (hashing)
- JWT Bearer · AspNetCoreRateLimit · Swagger
