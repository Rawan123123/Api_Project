# API_ITI – Simple ASP.NET Core Web API

This project is a simple **ASP.NET Core Web API** built as part of the ITI training content.  
It demonstrates the main concepts of:
- Routing  
- Controllers  
- Model Binding  
- DTOs  
- Entity Framework Core (Code-First)  
- Basic Authentication (Register & Login)  
- CRUD operations

---

## 📌 Project Structure
-Controllers/
-DTO/
-Models/
-Migrations/
-appsettings.json
-Program.cs

---

## 📌 Features & Endpoints

### 👤 **Account**
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/Account/Register` | Register a new user |
| POST | `/api/Account/Login` | Login and return success/fail |

---

### 🔗 **Binding**
Testing different types of model binding.

| Method | Endpoint |
|--------|----------|
| GET | `/api/Binding/{name}/{age}` |
| POST | `/api/Binding/{name}` |
| GET | `/api/Binding/{id}/{name}/{managerName}` |

---

### 🏢 **Department**
Basic CRUD operations.

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Department/Details` | Get department details |
| GET | `/api/Department` | List all departments |
| POST | `/api/Department` | Add new department |
| GET | `/api/Department/{id}` | Get department by id |
| PUT | `/api/Department/{id}` | Update department |
| GET | `/api/Department/{name}` | Get department by name |

---

### 👨‍💼 **Employee**
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Employee/{id}` | Get employee by id |

---

## 🗄️ Database
The project uses **Entity Framework Core** with **Code First Migration**.

To update the database:
```bash
Add-Migration Initial
Update-Database

