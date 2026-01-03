# MyApp - Complete Project Documentation

## 📋 Table of Contents
1. [Project Overview](#project-overview)
2. [Features](#features)
3. [Technology Stack](#technology-stack)
4. [Architecture](#architecture)
5. [Project Structure](#project-structure)
6. [Database Schema](#database-schema)
7. [Setup Instructions](#setup-instructions)
8. [Configuration](#configuration)
9. [API Documentation](#api-documentation)
10. [MVC Controllers](#mvc-controllers)
11. [Services Layer](#services-layer)
12. [Models & DTOs](#models--dtos)
13. [Authentication & Authorization](#authentication--authorization)
14. [Email Configuration](#email-configuration)
15. [File Upload Configuration](#file-upload-configuration)
16. [Development Guidelines](#development-guidelines)

---

## 🎯 Project Overview

**MyApp** is a comprehensive ASP.NET Core 10.0 web application that combines MVC (Model-View-Controller) architecture with RESTful API capabilities. The application provides a complete solution for managing movies, genres, users, and roles with full authentication and authorization support.

### Key Characteristics:
- **Framework**: ASP.NET Core 10.0
- **Architecture**: MVC + Web API (Hybrid)
- **Database**: SQL Server (LocalDB)
- **Authentication**: ASP.NET Core Identity
- **API Documentation**: Swagger/OpenAPI
- **UI Framework**: Razor Pages + Bootstrap

---

## ✨ Features

### Core Features:
1. **User Management**
   - User registration with email confirmation
   - User login/logout
   - Password reset functionality
   - User profile management
   - Custom user properties (FirstName, LastName, ProfilePicture)

2. **Role-Based Access Control (RBAC)**
   - Role creation and management
   - User-role assignment
   - Admin-only access to sensitive operations
   - Role-based authorization on controllers

3. **Movies Management**
   - CRUD operations for movies
   - Movie details (Title, Year, Rate, Storyline, Poster, Genre)
   - RESTful API endpoints
   - Genre association

4. **Genres Management**
   - CRUD operations for genres
   - RESTful API endpoints
   - Genre-movie relationship

5. **API Documentation**
   - Swagger UI integration
   - JWT Bearer token support (configured)
   - Custom Swagger styling

6. **Email Services**
   - Email confirmation for new users
   - Password reset emails
   - SMTP configuration (Gmail)

---

## 🛠 Technology Stack

### Core Technologies:
- **.NET 10.0** - Latest .NET framework
- **ASP.NET Core MVC** - Web framework
- **Entity Framework Core 10.1** - ORM
- **SQL Server** - Database (LocalDB for development)
- **ASP.NET Core Identity** - Authentication & Authorization

### NuGet Packages:
```xml
- Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore (10.0.1)
- Microsoft.AspNetCore.Identity.EntityFrameworkCore (10.0.1)
- Microsoft.AspNetCore.Identity.UI (10.0.1)
- Microsoft.EntityFrameworkCore.SqlServer (10.0.1)
- Microsoft.EntityFrameworkCore.Sqlite (10.0.0)
- Microsoft.EntityFrameworkCore.Tools (10.0.1)
- Microsoft.VisualStudio.Web.CodeGeneration.Design (10.0.0)
- Swashbuckle.AspNetCore (6.7.0)
```

### Frontend Technologies:
- **Bootstrap** - CSS framework
- **jQuery** - JavaScript library
- **jQuery Validation** - Form validation
- **Razor Pages** - Server-side rendering

---

## 🏗 Architecture

### Architecture Pattern:
The application follows a **layered architecture** with clear separation of concerns:

```
┌─────────────────────────────────────┐
│      Presentation Layer             │
│  (Controllers, Views, Razor Pages) │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│      Business Logic Layer           │
│      (Services & Interfaces)        │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│      Data Access Layer              │
│  (DbContext, Entity Framework)      │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│      Database Layer                 │
│         (SQL Server)                │
└─────────────────────────────────────┘
```

### Key Components:

1. **Controllers**
   - **MVC Controllers**: Handle web UI requests (Home, Account, Users, Roles)
   - **API Controllers**: Handle REST API requests (Movies, Genres)

2. **Services**
   - Business logic abstraction
   - Interface-based design for testability
   - Dependency injection

3. **Models**
   - Domain models (Movie, Genre, ApplicationUser)
   - DTOs (Data Transfer Objects) for API
   - ViewModels for MVC views

4. **Data Layer**
   - ApplicationDbContext
   - Entity Framework Core migrations
   - Custom schema organization (Security schema)

---

## 📁 Project Structure

```
MyApp/
│
├── Areas/
│   └── Identity/
│       └── Pages/              # Identity UI pages (scaffolded)
│
├── Controllers/
│   ├── APIControllers/         # REST API Controllers
│   │   ├── GenresController.cs
│   │   └── MoviesController.cs
│   │
│   └── MVCControllers/         # MVC Controllers
│       ├── AccountController.cs
│       ├── HomeController.cs
│       ├── RolesController.cs
│       └── UsersController.cs
│
├── Data/
│   ├── ApplicationDbContext.cs # Main DbContext
│   └── Migrations/             # EF Core migrations
│
├── DTOs/                       # Data Transfer Objects
│   ├── GenreDto.cs
│   └── MovieDto.cs
│
├── Models/
│   ├── APIModels/              # Domain models
│   │   ├── Genre.cs
│   │   └── Movie.cs
│   │
│   ├── UserModels/
│   │   └── ApplicationUser.cs
│   │
│   └── ErrorViewModel.cs
│
├── Services/
│   ├── Implementations/        # Service implementations
│   │   ├── EmailSender.cs
│   │   ├── GenresServices.cs
│   │   └── MoviesServices.cs
│   │
│   └── Interfaces/             # Service interfaces
│       ├── IGenresServices.cs
│       └── IMoviesServices.cs
│
├── ViewModels/                 # View models for MVC
│   ├── Accounts/
│   ├── Roles/
│   └── User/
│
├── Views/                      # Razor views
│   ├── Account/
│   ├── Home/
│   ├── Roles/
│   ├── Users/
│   └── Shared/
│
├── wwwroot/                    # Static files
│   ├── css/
│   ├── js/
│   ├── lib/                    # Third-party libraries
│   └── ProfileImages/
│
├── Properties/
│   └── launchSettings.json     # Launch configuration
│
├── appsettings.json            # Application settings
├── appsettings.Development.json
├── GlobalUsing.cs              # Global using directives
├── Program.cs                   # Application entry point
└── MyApp.csproj                # Project file
```

---

## 🗄 Database Schema

### Security Schema (ASP.NET Identity Tables):
All Identity tables are organized in the `Security` schema:

- **Users** (`Security.Users`)
  - Id (PK, string)
  - UserName
  - Email
  - EmailConfirmed
  - FirstName (custom)
  - LastName (custom)
  - ProfilePicture (custom, byte[])
  - PasswordHash
  - SecurityStamp
  - ... (other Identity fields)

- **Roles** (`Security.Roles`)
  - Id (PK, string)
  - Name
  - NormalizedName

- **UserRoles** (`Security.UserRoles`)
  - UserId (FK)
  - RoleId (FK)

- **UserClaims** (`Security.UserClaims`)
- **UserLogins** (`Security.UserLogins`)
- **UserTokens** (`Security.UserTokens`)
- **RoleClaims** (`Security.RoleClaims`)

### Application Tables:

#### Genres Table:
```sql
CREATE TABLE [Genres] (
    [Id] int IDENTITY(1,1) PRIMARY KEY,
    [Name] nvarchar(100) NOT NULL
)
```

#### Movies Table:
```sql
CREATE TABLE [Movies] (
    [Id] int IDENTITY(1,1) PRIMARY KEY,
    [Title] nvarchar(250) NOT NULL,
    [Year] int NOT NULL,
    [Rate] float NOT NULL,
    [StoreLuine] nvarchar(2500) NOT NULL,
    [Poster] nvarchar(max) NOT NULL,
    [GenreId] int NOT NULL,
    FOREIGN KEY ([GenreId]) REFERENCES [Genres]([Id])
)
```

### Entity Relationships:
- **Movie** → **Genre** (Many-to-One)
  - One Movie belongs to one Genre
  - One Genre can have many Movies

---

## 🚀 Setup Instructions

### Prerequisites:
1. **.NET 10.0 SDK** - [Download](https://dotnet.microsoft.com/download)
2. **SQL Server LocalDB** - Included with Visual Studio
3. **Visual Studio 2022** or **VS Code** (recommended)
4. **Git** (optional)

### Step-by-Step Setup:

#### 1. Clone/Navigate to Project
```bash
cd "D:\New folder\Backup\MyApp\MyApp"
```

#### 2. Restore Dependencies
```bash
dotnet restore
```

#### 3. Update Database Connection String
Edit `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MyApp;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

#### 4. Apply Database Migrations
```bash
dotnet ef database update
```

#### 5. Run the Application
```bash
dotnet run
```

Or use Visual Studio:
- Press `F5` or click "Run"

#### 6. Access the Application
- **Web UI**: `https://localhost:7085` or `http://localhost:5169`
- **Swagger UI**: `https://localhost:7085/swagger` (Development only)

---

## ⚙️ Configuration

### appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MyApp;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Program.cs Configuration Highlights:

#### Database Configuration:
```csharp
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
```

#### Identity Configuration:
```csharp
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = true; // Email confirmation required
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders()
.AddDefaultUI();
```

#### File Upload Limits:
- **Multipart Body Length**: 10 MB
- **Max Request Body Size**: 10 MB

#### CORS Configuration:
```csharp
app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());
```

#### Swagger Configuration:
- Available only in Development environment
- JWT Bearer token authentication configured
- Custom CSS styling support

---

## 📡 API Documentation

### Base URL:
- **Development**: `https://localhost:7085/api`
- **Production**: Configure based on deployment

### Authentication:
Currently, API endpoints are **not protected** by authentication. JWT Bearer token support is configured in Swagger but not enforced.

### API Endpoints:

#### Movies API

##### Get All Movies
```
GET /api/Movies
```
**Response:**
```json
[
  {
    "id": 1,
    "title": "Movie Title",
    "year": 2024,
    "rate": 8.5,
    "storeLuine": "Movie storyline...",
    "poster": "poster-url",
    "genreId": 1,
    "genre": {
      "id": 1,
      "name": "Action"
    }
  }
]
```

##### Get Movie by ID
```
GET /api/Movies/{id}
```
**Response:**
```json
{
  "id": 1,
  "title": "Movie Title",
  "year": 2024,
  "rate": 8.5,
  "storeLuine": "Movie storyline...",
  "poster": "poster-url",
  "genreId": 1,
  "genre": {
    "id": 1,
    "name": "Action"
  }
}
```
**Error Response (404):**
```json
{
  "message": "Movie with ID {id} not found."
}
```

##### Create Movie
```
POST /api/Movies
Content-Type: application/json
```
**Request Body:**
```json
{
  "title": "New Movie",
  "year": 2024,
  "rate": 7.5,
  "storeLuine": "Movie description...",
  "poster": "poster-url-or-path",
  "genreId": 1
}
```
**Response (201):**
```json
{
  "id": 2,
  "title": "New Movie",
  "year": 2024,
  "rate": 7.5,
  "storeLuine": "Movie description...",
  "poster": "poster-url-or-path",
  "genreId": 1,
  "genre": null
}
```

##### Update Movie
```
PUT /api/Movies/{id}
Content-Type: application/json
```
**Request Body:** (Same as Create)
**Response (200):** Updated movie object
**Error Response (404):** If movie not found

##### Delete Movie
```
DELETE /api/Movies/{id}
```
**Response (200):**
```json
{
  "message": "Movie Deleted Successfully"
}
```
**Error Response (404):** If movie not found

---

#### Genres API

##### Get All Genres
```
GET /api/Genres
```
**Response:**
```json
[
  {
    "id": 1,
    "name": "Action"
  },
  {
    "id": 2,
    "name": "Comedy"
  }
]
```

##### Get Genre by ID
```
GET /api/Genres/{id}
```
**Response:**
```json
{
  "id": 1,
  "name": "Action"
}
```

##### Create Genre
```
POST /api/Genres
Content-Type: application/json
```
**Request Body:**
```json
{
  "name": "Thriller"
}
```
**Response (201):** Created genre object

##### Update Genre
```
PUT /api/Genres/{id}
Content-Type: application/json
```
**Request Body:**
```json
{
  "name": "Updated Genre Name"
}
```

##### Delete Genre
```
DELETE /api/Genres/{id}
```
**Response (200):**
```json
{
  "message": "Genre Deleted Successfully"
}
```

---

## 🎮 MVC Controllers

### HomeController
**Route:** `/Home`
- **Index** - Home page
- **About** - About page
- **Contact** - Contact page
- **Privacy** - Privacy policy page
- **Error** - Error page

### AccountController
**Route:** `/Account`
- Handles user authentication (Login, Register, Logout)
- Email confirmation
- Password reset functionality
- Uses ASP.NET Core Identity UI

### UsersController
**Authorization:** `[Authorize(Roles = "Admin")]`
**Route:** `/Users`

**Actions:**
- **Index** (GET) - List all users with their roles
- **ADD** (GET/POST) - Create new user with role assignment
- **Edit** (GET/POST) - Update user information
- **ManageRoles** (GET) - View user roles
- **UpdateRoles** (POST) - Update user roles
- **Delete** (GET/POST) - Delete user

### RolesController
**Authorization:** `[Authorize(Roles = "Admin")]`
**Route:** `/Roles`

**Actions:**
- **Index** (GET) - List all roles
- **RoleForm** (GET) - Display role creation form
- **ADD** (POST) - Create new role
- **Edit** (GET/POST) - Update role name
- **Delete** (GET/POST) - Delete role

---

## 🔧 Services Layer

### Service Pattern:
The application uses the **Service Pattern** with interfaces and implementations for better testability and maintainability.

### IMoviesServices & MoviesServices

**Interface Methods:**
```csharp
Task<List<Movie>> GetAll();
Task<Movie?> GetById(int id);
Task<Movie> Create(MovieDto dto);
Task<Movie?> Update(int id, MovieDto dto);
Task<bool> Delete(int id);
```

**Implementation Details:**
- Uses `ApplicationDbContext` for data access
- Includes Genre navigation property in queries
- Async/await pattern throughout
- Returns `null` for not found entities

**Registration:**
```csharp
builder.Services.AddTransient<IMoviesServices, MoviesServices>();
```

### IGenresServices & GenresServices

**Interface Methods:**
```csharp
Task<List<Genre>> GetAll();
Task<Genre?> GetById(int id);
Task<Genre> Create(GenreDto dto);
Task<Genre?> Update(int id, GenreDto dto);
Task<bool> Delete(int id);
```

**Implementation Details:**
- Simple CRUD operations
- Async/await pattern
- Returns `null` for not found entities

**Registration:**
```csharp
builder.Services.AddTransient<IGenresServices, GenresServices>();
```

### EmailSender

**Implementation:**
- Implements `IEmailSender` from ASP.NET Core Identity
- Uses SMTP (Gmail) for sending emails
- Configured for email confirmation and password reset

**Configuration:**
```csharp
SmtpClient: smtp.gmail.com
Port: 587
EnableSsl: true
From: hazemosama322@gmail.com
```

**⚠️ Security Note:** Email credentials are hardcoded. In production, use:
- User Secrets (Development)
- Azure Key Vault (Production)
- Environment Variables

**Registration:**
```csharp
builder.Services.AddScoped<IEmailSender, EmailSender>();
```

---

## 📦 Models & DTOs

### Domain Models

#### ApplicationUser
**Location:** `Models/UserModels/ApplicationUser.cs`
**Inherits:** `IdentityUser`

**Properties:**
```csharp
public string? FirstName { get; set; }        // MaxLength: 100, Required
public string? LastName { get; set; }         // MaxLength: 100, Required
public byte[]? ProfilePicture { get; set; }   // Optional
```

**Inherited Properties:**
- Id (string)
- UserName
- Email
- EmailConfirmed
- PasswordHash
- ... (all IdentityUser properties)

#### Movie
**Location:** `Models/APIModels/Movie.cs`

**Properties:**
```csharp
public int Id { get; set; }
public string Title { get; set; }              // MaxLength: 250
public int Year { get; set; }
public double Rate { get; set; }
public string StoreLuine { get; set; }        // MaxLength: 2500 (Storyline)
public string Poster { get; set; }            // Poster URL/path
public int GenreId { get; set; }              // Foreign Key
public Genre Genre { get; set; }              // Navigation Property
```

#### Genre
**Location:** `Models/APIModels/Genre.cs`

**Properties:**
```csharp
[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public int Id { get; set; }
public string Name { get; set; }              // MaxLength: 100
```

### Data Transfer Objects (DTOs)

#### MovieDto
**Location:** `DTOs/MovieDto.cs`
**Purpose:** Used for API requests (Create/Update)

**Properties:**
```csharp
public string Title { get; set; }             // MaxLength: 250
public int Year { get; set; }
public double Rate { get; set; }
public string StoreLuine { get; set; }        // MaxLength: 2500
public string Poster { get; set; }
public int GenreId { get; set; }
```

#### GenreDto
**Location:** `DTOs/GenreDto.cs`
**Purpose:** Used for API requests (Create/Update)

**Properties:**
```csharp
public string Name { get; set; }              // MaxLength: 100
```

### ViewModels

ViewModels are used for MVC views and are located in `ViewModels/`:
- **Accounts**: LoginViewModel, RegisterViewModel, ForgotPasswordViewModel, ResetPasswordViewModel
- **Roles**: RoleFormViewModel, RoleViewModel
- **User**: UserViewModel, ADDUserViewModel, EditUserViewModel, UserRolesVieweModel

---

## 🔐 Authentication & Authorization

### ASP.NET Core Identity Configuration

#### Identity Setup:
```csharp
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = true; // Email must be confirmed
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders()
.AddDefaultUI();
```

### Identity Features:
1. **Email Confirmation Required**
   - Users must confirm email before login
   - Email sent via `EmailSender` service

2. **Password Reset**
   - Forgot password functionality
   - Reset password via email link

3. **Role-Based Authorization**
   - Roles stored in `Security.Roles` table
   - User-Role mapping in `Security.UserRoles`
   - Admin role required for Users and Roles controllers

### Authorization Attributes:

#### Role-Based Authorization:
```csharp
[Authorize(Roles = "Admin")]
public class UsersController : Controller { }
```

#### Usage Examples:
- **Allow Anonymous:**
  ```csharp
  [AllowAnonymous]
  public IActionResult PublicPage() { }
  ```

- **Require Authentication:**
  ```csharp
  [Authorize]
  public IActionResult ProtectedPage() { }
  ```

- **Require Specific Role:**
  ```csharp
  [Authorize(Roles = "Admin")]
  public IActionResult AdminOnly() { }
  ```

### Default Roles:
Roles are seeded via migrations. Check migration files in `Data/Migrations/` for seeded roles.

---

## 📧 Email Configuration

### EmailSender Service

**Location:** `Services/Implementations/EmailSender.cs`

**Current Configuration:**
- **SMTP Server:** smtp.gmail.com
- **Port:** 587
- **SSL:** Enabled
- **From Email:** hazemosama322@gmail.com
- **Authentication:** App Password (Gmail)

### Email Usage:
1. **Email Confirmation**
   - Sent when user registers
   - Contains confirmation link
   - Required before login

2. **Password Reset**
   - Sent when user requests password reset
   - Contains reset token link

### Production Recommendations:

#### Option 1: User Secrets (Development)
```bash
dotnet user-secrets set "EmailSettings:SmtpServer" "smtp.gmail.com"
dotnet user-secrets set "EmailSettings:Port" "587"
dotnet user-secrets set "EmailSettings:Username" "your-email@gmail.com"
dotnet user-secrets set "EmailSettings:Password" "your-app-password"
```

#### Option 2: appsettings.json (Development)
```json
{
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "Port": 587,
    "Username": "your-email@gmail.com",
    "Password": "your-app-password",
    "FromEmail": "your-email@gmail.com",
    "FromName": "MyApp"
  }
}
```

#### Option 3: Environment Variables (Production)
Set environment variables:
- `EmailSettings__SmtpServer`
- `EmailSettings__Port`
- `EmailSettings__Username`
- `EmailSettings__Password`

---

## 📤 File Upload Configuration

### Current Limits:
- **Multipart Body Length:** 10 MB
- **Max Request Body Size:** 10 MB

### Configuration in Program.cs:
```csharp
// Form options
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // 10 MB
});

// Kestrel server options
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 10 * 1024 * 1024; // 10 MB
});
```

### Profile Picture Storage:
- Currently stored as `byte[]` in `ApplicationUser.ProfilePicture`
- Default image: `wwwroot/ProfileImages/defultimg.jpg`

### Recommendations:
For production, consider:
1. **File Storage Service** (Azure Blob Storage, AWS S3)
2. **File System Storage** with path in database
3. **CDN Integration** for better performance

---

## 📝 Development Guidelines

### Code Organization:
1. **Separation of Concerns**
   - Controllers handle HTTP requests/responses
   - Services contain business logic
   - Models represent data structures
   - DTOs for API data transfer

2. **Naming Conventions**
   - Controllers: `[Name]Controller`
   - Services: `[Name]Services` (Interface: `I[Name]Services`)
   - Models: PascalCase
   - DTOs: `[Name]Dto`

3. **Async/Await Pattern**
   - All database operations use async/await
   - Service methods return `Task<T>`

### Global Usings:
Defined in `GlobalUsing.cs`:
```csharp
global using System.ComponentModel.DataAnnotations;
global using MyApp.Data;
global using MyApp.DTOs;
global using MyApp.Models.APIModels;
global using MyApp.Models.UserModels;
global using Microsoft.AspNetCore.Builder;
global using MyApp.Services.Interfaces;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Mvc;
global using MyApp.ViewModels;
```

### Database Migrations:

#### Create Migration:
```bash
dotnet ef migrations add MigrationName
```

#### Apply Migration:
```bash
dotnet ef database update
```

#### Remove Last Migration:
```bash
dotnet ef migrations remove
```

### Dependency Injection:
- **Transient:** Services (created per request)
- **Scoped:** EmailSender (created per HTTP request)
- **Singleton:** Not used in this project

### Error Handling:
- API Controllers return appropriate HTTP status codes
- MVC Controllers use ModelState for validation errors
- Global error handling via `UseExceptionHandler` in production

### Logging:
Configured in `appsettings.json`:
- Default: Information
- Microsoft.AspNetCore: Warning

---

## 🔍 Key Files Reference

### Program.cs
- Application entry point
- Service registration
- Middleware configuration
- Routing setup

### ApplicationDbContext.cs
- Database context
- Entity configurations
- Schema organization

### GlobalUsing.cs
- Global namespace imports
- Reduces repetitive using statements

### launchSettings.json
- Development server configuration
- Environment variables
- Application URLs

---

## 🚨 Security Considerations

### Current Security Measures:
1. ✅ Email confirmation required
2. ✅ Password hashing (Identity)
3. ✅ Role-based authorization
4. ✅ HTTPS in production
5. ✅ CORS configuration
6. ✅ Anti-forgery tokens (MVC)

### Security Recommendations:
1. ⚠️ **Move email credentials to secure storage**
2. ⚠️ **Implement JWT authentication for API**
3. ⚠️ **Add rate limiting**
4. ⚠️ **Implement input validation**
5. ⚠️ **Add SQL injection protection** (EF Core provides this)
6. ⚠️ **Enable HTTPS redirect in production**
7. ⚠️ **Configure CORS properly for production**
8. ⚠️ **Add logging for security events**

---

## 🧪 Testing Recommendations

### Unit Testing:
- Test Services layer
- Mock ApplicationDbContext
- Test business logic

### Integration Testing:
- Test API endpoints
- Test MVC controllers
- Test authentication flows

### Tools:
- **xUnit** - Unit testing framework
- **Moq** - Mocking framework
- **Microsoft.AspNetCore.Mvc.Testing** - Integration testing

---

## 📚 Additional Resources

### Official Documentation:
- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [ASP.NET Core Identity](https://docs.microsoft.com/aspnet/core/security/authentication/identity)
- [Swagger/OpenAPI](https://swagger.io/specification/)

### Learning Resources:
- [ASP.NET Core Tutorial](https://dotnet.microsoft.com/learn/aspnet)
- [Entity Framework Core Tutorial](https://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx)

---

## 📞 Support & Contact

For issues, questions, or contributions:
1. Check existing documentation
2. Review code comments
3. Check migration history for database changes
4. Review Swagger documentation for API details

---

## 📄 License

This project is provided as-is for educational and development purposes.

---

## 🎉 Conclusion

This documentation provides a comprehensive overview of the MyApp project. The application is a well-structured ASP.NET Core application with MVC and API capabilities, featuring user management, role-based access control, and movie/genre management.

For specific implementation details, refer to the source code and inline comments.

**Last Updated:** 2024
**Version:** 1.0
**Framework:** .NET 10.0

