# Architecture Documentation - MyApp

This document provides detailed technical architecture information for the MyApp project.

---

## Table of Contents

1. [System Architecture](#system-architecture)
2. [Application Architecture](#application-architecture)
3. [Data Flow](#data-flow)
4. [Dependency Injection](#dependency-injection)
5. [Database Design](#database-design)
6. [Security Architecture](#security-architecture)
7. [API Architecture](#api-architecture)
8. [MVC Architecture](#mvc-architecture)
9. [Service Layer Pattern](#service-layer-pattern)
10. [Error Handling](#error-handling)
11. [Performance Considerations](#performance-considerations)

---

## System Architecture

### High-Level Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                      Client Layer                           │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐     │
│  │   Web UI     │  │  Mobile App  │  │  API Client  │     │
│  │  (Browser)   │  │  (Future)     │  │  (Postman)   │     │
│  └──────┬───────┘  └──────┬───────┘  └──────┬───────┘     │
└─────────┼──────────────────┼──────────────────┼─────────────┘
          │                  │                  │
          └──────────────────┼──────────────────┘
                             │
          ┌──────────────────▼──────────────────┐
          │      ASP.NET Core Application        │
          │  ┌────────────────────────────────┐  │
          │  │      Presentation Layer         │  │
          │  │  ┌──────────┐  ┌─────────────┐ │  │
          │  │  │   MVC    │  │     API     │ │  │
          │  │  │Controllers│ │Controllers  │ │  │
          │  │  └────┬─────┘  └──────┬──────┘ │  │
          │  └───────┼───────────────┼────────┘  │
          │          │               │           │
          │  ┌───────▼───────────────▼────────┐  │
          │  │      Business Logic Layer       │  │
          │  │  ┌──────────┐  ┌─────────────┐ │  │
          │  │  │ Services │  │  Interfaces │ │  │
          │  │  └────┬─────┘  └──────┬──────┘ │  │
          │  └───────┼───────────────┼────────┘  │
          │          │               │           │
          │  ┌───────▼───────────────▼────────┐  │
          │  │      Data Access Layer          │  │
          │  │  ┌──────────────────────────┐  │  │
          │  │  │  ApplicationDbContext     │  │  │
          │  │  │  Entity Framework Core    │  │  │
          │  │  └───────┬──────────────────┘  │  │
          │  └──────────┼─────────────────────┘  │
          └─────────────┼─────────────────────────┘
                        │
          ┌─────────────▼─────────────┐
          │      Database Layer       │
          │    SQL Server (LocalDB)   │
          └───────────────────────────┘
```

### Technology Stack Layers

| Layer | Technology | Purpose |
|-------|-----------|---------|
| **Presentation** | ASP.NET Core MVC, Razor Pages | User interface rendering |
| **API** | ASP.NET Core Web API | RESTful API endpoints |
| **Business Logic** | C# Services | Business rules and logic |
| **Data Access** | Entity Framework Core | Database operations |
| **Database** | SQL Server | Data persistence |
| **Authentication** | ASP.NET Core Identity | User authentication & authorization |

---

## Application Architecture

### Request Flow

#### MVC Request Flow:
```
1. HTTP Request → 
2. Routing Middleware → 
3. Controller Action → 
4. Service Layer → 
5. DbContext → 
6. Database → 
7. Return Data → 
8. View Model → 
9. Razor View → 
10. HTML Response
```

#### API Request Flow:
```
1. HTTP Request → 
2. Routing Middleware → 
3. API Controller → 
4. Service Layer → 
5. DbContext → 
6. Database → 
7. Return Data → 
8. JSON Serialization → 
9. JSON Response
```

### Middleware Pipeline

The middleware pipeline in `Program.cs` is executed in the following order:

```csharp
1. Exception Handler (Production only)
2. HTTPS Redirection
3. Static Files
4. Routing
5. CORS
6. Authentication
7. Authorization
8. Endpoints (Controllers, Razor Pages)
```

**Visual Representation:**
```
Request
  │
  ├─→ Exception Handler (if Production)
  │
  ├─→ HTTPS Redirection
  │
  ├─→ Static Files
  │
  ├─→ Routing
  │
  ├─→ CORS
  │
  ├─→ Authentication
  │
  ├─→ Authorization
  │
  └─→ Endpoints
      ├─→ MVC Controllers
      ├─→ API Controllers
      └─→ Razor Pages
```

---

## Data Flow

### Create Movie Flow (Example)

```
┌─────────────┐
│   Client    │
│  (Browser)  │
└──────┬──────┘
       │ POST /api/Movies
       │ { title, year, rate, ... }
       ▼
┌─────────────────┐
│ MoviesController│
│  CreateAsync()  │
└──────┬──────────┘
       │ Validate ModelState
       │
       ▼
┌─────────────────┐
│ MoviesServices  │
│   Create()      │
└──────┬──────────┘
       │ Create Movie entity
       │
       ▼
┌─────────────────┐
│ApplicationDbContext│
│   Movies.Add()  │
└──────┬──────────┘
       │ SaveChangesAsync()
       │
       ▼
┌─────────────────┐
│   SQL Server    │
│  INSERT INTO    │
└──────┬──────────┘
       │ Return Movie
       │
       ▼
┌─────────────────┐
│ MoviesServices  │
│  Return Movie   │
└──────┬──────────┘
       │
       ▼
┌─────────────────┐
│ MoviesController│
│  CreatedAtAction│
└──────┬──────────┘
       │ 201 Created
       │ + Location header
       ▼
┌─────────────┐
│   Client    │
│  Receives   │
│   Response  │
└─────────────┘
```

---

## Dependency Injection

### Service Lifetime

The application uses three service lifetimes:

#### 1. Transient
**Used for:** Business services
```csharp
builder.Services.AddTransient<IMoviesServices, MoviesServices>();
builder.Services.AddTransient<IGenresServices, GenresServices>();
```
**Lifetime:** New instance created every time it's requested
**Use Case:** Stateless services with no shared state

#### 2. Scoped
**Used for:** Request-specific services
```csharp
builder.Services.AddScoped<IEmailSender, EmailSender>();
```
**Lifetime:** One instance per HTTP request
**Use Case:** Services that need to maintain state during a request

#### 3. Singleton
**Not used in this project**
**Lifetime:** One instance for the entire application lifetime

### Dependency Graph

```
ApplicationDbContext (Scoped - by EF Core)
    ↑
    │
MoviesServices (Transient)
    ↑
    │
MoviesController (Transient - by framework)
    ↑
    │
HTTP Request
```

### Service Registration Pattern

```csharp
// Interface → Implementation mapping
IMoviesServices → MoviesServices
IGenresServices → GenresServices
IEmailSender → EmailSender
ApplicationDbContext → (Registered by EF Core)
```

---

## Database Design

### Schema Organization

#### Security Schema
All ASP.NET Core Identity tables are organized in the `Security` schema:

```
Security.Users
Security.Roles
Security.UserRoles
Security.UserClaims
Security.UserLogins
Security.UserTokens
Security.RoleClaims
```

**Benefits:**
- Clear separation of security-related tables
- Easier to manage permissions
- Better organization

#### Default Schema
Application tables use the default schema:

```
Genres
Movies
```

### Entity Relationships

```
┌─────────────┐         ┌─────────────┐
│   Genre     │         │   Movie     │
├─────────────┤         ├─────────────┤
│ Id (PK)     │◄────────┤ GenreId (FK)│
│ Name        │   1     │ Id (PK)     │
└─────────────┘    │    │ Title       │
                   │    │ Year        │
                   │    │ Rate        │
                   │    │ StoreLuine  │
                   │    │ Poster      │
                   │    └─────────────┘
                   │
                   │ Many
                   │
            One Genre can have Many Movies
```

### Database Context Configuration

```csharp
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Customize Identity table names and schema
        builder.Entity<ApplicationUser>().ToTable("Users", "Security");
        builder.Entity<IdentityRole>().ToTable("Roles", "Security");
        // ... other Identity tables
    }
}
```

---

## Security Architecture

### Authentication Flow

```
┌─────────────┐
│   User      │
│  Registers  │
└──────┬──────┘
       │
       ▼
┌─────────────────┐
│ Identity       │
│ Create User    │
└──────┬─────────┘
       │
       ▼
┌─────────────────┐
│ EmailSender    │
│ Send Confirmation│
└──────┬─────────┘
       │
       ▼
┌─────────────────┐
│ User Confirms   │
│ Email           │
└──────┬─────────┘
       │
       ▼
┌─────────────────┐
│ User Can Login │
└─────────────────┘
```

### Authorization Levels

#### 1. Anonymous Access
- Home pages
- Registration
- Login
- Public API endpoints (currently)

#### 2. Authenticated Access
- User profile pages
- Protected MVC actions

#### 3. Role-Based Access
- **Admin Role Required:**
  - `/Users/*` - User management
  - `/Roles/*` - Role management

### Security Middleware Order

```
Request
  │
  ├─→ CORS (Allow cross-origin requests)
  │
  ├─→ Authentication (Identify user)
  │     └─→ Cookie Authentication
  │     └─→ JWT (if configured)
  │
  └─→ Authorization (Check permissions)
        └─→ Role-based checks
        └─→ Policy-based checks
```

---

## API Architecture

### RESTful Design Principles

The API follows REST conventions:

| HTTP Method | Purpose | Example |
|------------|---------|---------|
| GET | Retrieve resource(s) | `GET /api/Movies` |
| POST | Create resource | `POST /api/Movies` |
| PUT | Update resource | `PUT /api/Movies/1` |
| DELETE | Delete resource | `DELETE /api/Movies/1` |

### API Controller Structure

```csharp
[Route("api/[controller]")]  // Base route: /api/Movies
[ApiController]                // Enables API-specific features
public class MoviesController : ControllerBase
{
    private readonly IMoviesServices _moviesServices;
    
    // Dependency injection via constructor
    public MoviesController(IMoviesServices moviesServices)
    {
        _moviesServices = moviesServices;
    }
    
    // Actions with HTTP method attributes
    [HttpGet]
    public async Task<IActionResult> GetAllMovies() { }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) { }
    
    // ... other actions
}
```

### Response Patterns

#### Success Responses:
- **200 OK** - Successful GET, PUT, DELETE
- **201 Created** - Successful POST with Location header

#### Error Responses:
- **400 Bad Request** - Validation errors
- **404 Not Found** - Resource not found
- **500 Internal Server Error** - Server errors

---

## MVC Architecture

### MVC Pattern Implementation

```
Model (Data + Business Logic)
  │
  │
Controller (Handles Requests)
  │
  │
View (Presentation)
```

### Controller Responsibilities

1. **Receive HTTP Requests**
2. **Validate Input** (ModelState)
3. **Call Services** (Business Logic)
4. **Prepare ViewModels**
5. **Return Views or Redirects**

### View Model Pattern

Instead of passing domain models directly to views, ViewModels are used:

```csharp
// Domain Model (not sent to view)
public class ApplicationUser : IdentityUser { }

// ViewModel (sent to view)
public class UserViewModel
{
    public string id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }
}
```

**Benefits:**
- Separation of concerns
- Security (don't expose internal properties)
- Flexibility (can combine multiple models)

---

## Service Layer Pattern

### Purpose

The Service Layer:
- Encapsulates business logic
- Provides abstraction over data access
- Enables testability
- Promotes reusability

### Interface-Based Design

```csharp
// Interface (Contract)
public interface IMoviesServices
{
    Task<List<Movie>> GetAll();
    Task<Movie?> GetById(int id);
    Task<Movie> Create(MovieDto dto);
    Task<Movie?> Update(int id, MovieDto dto);
    Task<bool> Delete(int id);
}

// Implementation
public class MoviesServices : IMoviesServices
{
    private readonly ApplicationDbContext _context;
    
    public MoviesServices(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // Implement interface methods
}
```

### Benefits

1. **Testability:** Easy to mock interfaces
2. **Flexibility:** Can swap implementations
3. **Separation:** Business logic separate from controllers
4. **Reusability:** Services can be used by multiple controllers

---

## Error Handling

### Current Error Handling Strategy

#### API Controllers:
```csharp
[HttpGet("{id}")]
public async Task<IActionResult> GetById(int id)
{
    var movie = await _moviesServices.GetById(id);
    if (movie == null) 
        return NotFound($"Movie with ID {id} not found.");
    return Ok(movie);
}
```

#### MVC Controllers:
```csharp
[HttpPost]
public async Task<IActionResult> Create(ViewModel model)
{
    if (!ModelState.IsValid)
        return View(model);
    
    // Process...
}
```

#### Global Error Handling:
```csharp
if (app.Environment.IsDevelopment())
{
    // Show detailed errors
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
```

### Recommended Enhancements

1. **Global Exception Middleware**
2. **Structured Logging**
3. **Error Response Standardization**
4. **Validation Error Formatting**

---

## Performance Considerations

### Current Optimizations

1. **Async/Await Pattern**
   - All database operations are asynchronous
   - Non-blocking I/O operations

2. **Eager Loading**
   ```csharp
   return await _context.Movies
       .Include(m => m.Genre)  // Load related data
       .ToListAsync();
   ```

3. **Connection Pooling**
   - EF Core manages connection pooling automatically

### Potential Improvements

1. **Caching**
   - Cache frequently accessed data (genres, etc.)
   - Use MemoryCache or Redis

2. **Pagination**
   - Implement pagination for list endpoints
   - Reduce data transfer

3. **Database Indexing**
   - Add indexes on frequently queried columns
   - Foreign key indexes (usually automatic)

4. **Query Optimization**
   - Use `Select()` to project only needed fields
   - Avoid N+1 queries

5. **Response Compression**
   - Enable response compression middleware

---

## Code Organization Principles

### Separation of Concerns

```
Controllers    → Handle HTTP requests/responses
Services       → Business logic
Models         → Domain entities
DTOs           → Data transfer objects
ViewModels     → View-specific models
DbContext      → Data access
```

### Naming Conventions

- **Controllers:** `[Name]Controller`
- **Services:** `[Name]Services` (Interface: `I[Name]Services`)
- **Models:** PascalCase (e.g., `Movie`, `Genre`)
- **DTOs:** `[Name]Dto`
- **ViewModels:** `[Name]ViewModel`

### File Organization

- Group related files in folders
- Use namespaces that match folder structure
- Keep files focused on single responsibility

---

## Future Architecture Considerations

### Recommended Enhancements

1. **Repository Pattern**
   - Abstract data access further
   - Easier to swap data sources

2. **Unit of Work Pattern**
   - Manage transactions
   - Coordinate multiple repositories

3. **CQRS (Command Query Responsibility Segregation)**
   - Separate read and write operations
   - Better scalability

4. **API Versioning**
   - Support multiple API versions
   - Backward compatibility

5. **Microservices Architecture** (if scaling)
   - Split into separate services
   - Independent deployment

---

## Conclusion

The MyApp architecture follows modern ASP.NET Core best practices with:
- Clear separation of concerns
- Dependency injection
- Service layer pattern
- RESTful API design
- Secure authentication/authorization

The architecture is designed to be:
- **Maintainable:** Clear structure and patterns
- **Testable:** Interface-based design
- **Scalable:** Can be extended as needed
- **Secure:** Identity-based security

---

**Last Updated:** 2024

