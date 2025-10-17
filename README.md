# KalraFintech AuthService

A simple **Authentication microservice** for KalraFintech using ASP.NET Core, MySQL, and JWT-based authentication with role-based authorization.

---

## Features

- User registration and login
- Password hashing using BCrypt
- JWT token generation
- Role-based authorization
- Sample protected endpoints

---

## Project Structure

Controllers/ - API controllers
Models/ - Entity models
DTOs/ - Data transfer objects
Repositories/ - Database access layer
Services/ - Business logic
Utils/ - JWT utility and helpers
appsettings.json - Configuration file
Program.cs - Main entry


---

## Getting Started

### Prerequisites

- .NET 8 SDK or later
- MySQL server
- Visual Studio 2022 / VS Code / any IDE

### Setup

1. Clone the repo:

```bash
git clone https://github.com/<your-username>/KalraFintech.AuthService.git
cd KalraFintech.AuthService
```

2. Install Dependencies:

```bash
dotnet restore
```

3. Update appsettings.json:

```bash
"ConnectionStrings": {
    "DefaultConnection": "server=localhost;database=KalraFintechAuth;user=root;password=root;"
},
"JwtSettings": {
    "SecretKey": "YourVeryStrongSecretKeyThatIsAtLeast32CharsLong",
    "Issuer": "KalraFinTech",
    "Audience": "KalraFinTechUsers",
    "ExpiryMinutes": 60
}
```

4. Run migrations to create the database schema:

```bash
dotnet ef database update
```

5. Run the project:

```bash
dotnet run
```

## Testing the API

Login endpoint: POST /api/auth/login

Register endpoint: POST /api/auth/register

Protected endpoint: GET /api/test/protected (requires JWT)

Admin-only endpoint: GET /api/test/admin (requires SuperAdmin role)

Public endpoint: GET /api/test/public (no auth required)

Use Postman or any API client to test.


