# Setup Guide - MyApp

This guide will walk you through setting up the MyApp project from scratch.

---

## Prerequisites

Before you begin, ensure you have the following installed:

### Required Software:

1. **.NET 10.0 SDK**
   - Download from: https://dotnet.microsoft.com/download/dotnet/10.0
   - Verify installation:
     ```bash
     dotnet --version
     ```
   - Should output: `10.0.x` or higher

2. **SQL Server LocalDB** (or SQL Server Express)
   - Usually comes with Visual Studio
   - Or download SQL Server Express: https://www.microsoft.com/sql-server/sql-server-downloads
   - Verify installation:
     ```bash
     sqllocaldb info
     ```

3. **Visual Studio 2022** (Recommended)
   - Community Edition (Free): https://visualstudio.microsoft.com/downloads/
   - Or **Visual Studio Code** with C# extension
   - Or any code editor of your choice

### Optional but Recommended:

- **Git** - For version control
- **Postman** - For API testing
- **SQL Server Management Studio (SSMS)** - For database management

---

## Step-by-Step Setup

### Step 1: Verify Project Location

Navigate to the project directory:
```bash
cd "D:\New folder\Backup\MyApp\MyApp"
```

Or if using Git:
```bash
git clone <repository-url>
cd MyApp/MyApp
```

### Step 2: Verify .NET SDK

Check that .NET 10.0 is installed:
```bash
dotnet --version
```

If not installed, download and install from the link above.

### Step 3: Restore NuGet Packages

Restore all project dependencies:
```bash
dotnet restore
```

**Expected Output:**
```
Determining projects to restore...
Restored MyApp.csproj (in X ms).
```

### Step 4: Configure Database Connection

#### Option A: Use LocalDB (Default - Recommended for Development)

The default connection string in `appsettings.json` should work:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MyApp;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

#### Option B: Use SQL Server Express

If you prefer SQL Server Express, update `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=MyApp;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

#### Option C: Use SQL Server (Full Instance)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MyApp;User Id=sa;Password=YourPassword;Trusted_Connection=False;MultipleActiveResultSets=true"
  }
}
```

### Step 5: Apply Database Migrations

Create and apply the database schema:
```bash
dotnet ef database update
```

**Expected Output:**
```
Build started...
Build succeeded.
Applying migration '00000000000000_CreateIdentitySchema'.
Applying migration '20251221222744_RenameTablesAndAddFirstNameAndLastName'.
...
Done.
```

**Troubleshooting:**
- If `dotnet ef` command not found, install EF Core tools:
  ```bash
  dotnet tool install --global dotnet-ef
  ```
- If database connection fails, verify SQL Server is running
- If LocalDB not found, try:
  ```bash
  sqllocaldb create MSSQLLocalDB
  sqllocaldb start MSSQLLocalDB
  ```

### Step 6: Verify Database Creation

#### Using SQL Server Management Studio (SSMS):
1. Open SSMS
2. Connect to: `(localdb)\MSSQLLocalDB`
3. Expand Databases
4. Verify `MyApp` database exists
5. Check tables in `Security` schema and default schema

#### Using Command Line:
```bash
sqlcmd -S "(localdb)\MSSQLLocalDB" -Q "SELECT name FROM sys.databases WHERE name = 'MyApp'"
```

### Step 7: Configure Email Settings (Optional but Recommended)

#### For Development - Use User Secrets:

```bash
dotnet user-secrets init
dotnet user-secrets set "EmailSettings:SmtpServer" "smtp.gmail.com"
dotnet user-secrets set "EmailSettings:Port" "587"
dotnet user-secrets set "EmailSettings:Username" "your-email@gmail.com"
dotnet user-secrets set "EmailSettings:Password" "your-app-password"
dotnet user-secrets set "EmailSettings:FromEmail" "your-email@gmail.com"
dotnet user-secrets set "EmailSettings:FromName" "MyApp"
```

**Note:** For Gmail, you need to:
1. Enable 2-Factor Authentication
2. Generate an App Password: https://myaccount.google.com/apppasswords
3. Use the App Password (not your regular password)

#### Or Update EmailSender.cs Directly (Not Recommended for Production):

Edit `Services/Implementations/EmailSender.cs`:
```csharp
Credentials = new NetworkCredential(
    "your-email@gmail.com", 
    "your-app-password"
),
```

### Step 8: Build the Project

Build the project to check for compilation errors:
```bash
dotnet build
```

**Expected Output:**
```
Build succeeded.
    0 Warning(s)
    0 Error(s)
```

### Step 9: Run the Application

#### Option A: Using Command Line
```bash
dotnet run
```

#### Option B: Using Visual Studio
1. Open `MyApp.slnx` in Visual Studio
2. Press `F5` or click "Run"

#### Option C: Using Visual Studio Code
1. Open the project folder
2. Press `F5`
3. Select ".NET Core" from the debug options

**Expected Output:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:7085
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5169
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

### Step 10: Access the Application

1. **Web Application:**
   - Open browser: `https://localhost:7085`
   - Or: `http://localhost:5169`

2. **Swagger API Documentation:**
   - Open: `https://localhost:7085/swagger`
   - Only available in Development mode

3. **Identity Pages:**
   - Register: `https://localhost:7085/Identity/Account/Register`
   - Login: `https://localhost:7085/Identity/Account/Login`

---

## Initial Setup Tasks

### 1. Create Admin User

#### Option A: Using the Application UI
1. Register a new user at `/Identity/Account/Register`
2. Confirm email (if email is configured)
3. Login to the application
4. Manually add Admin role to the user in database (see below)

#### Option B: Using Database
1. Register a user through the UI
2. Open SQL Server Management Studio
3. Connect to your database
4. Run:
   ```sql
   -- Find your user ID
   SELECT Id, UserName, Email FROM [Security].[Users]

   -- Insert Admin role if it doesn't exist
   IF NOT EXISTS (SELECT 1 FROM [Security].[Roles] WHERE Name = 'Admin')
   BEGIN
       INSERT INTO [Security].[Roles] (Id, Name, NormalizedName, ConcurrencyStamp)
       VALUES (NEWID(), 'Admin', 'ADMIN', NEWID())
   END

   -- Assign Admin role to user (replace 'USER_ID' with actual user ID)
   DECLARE @UserId NVARCHAR(450) = 'USER_ID'
   DECLARE @RoleId NVARCHAR(450) = (SELECT Id FROM [Security].[Roles] WHERE Name = 'Admin')
   
   IF NOT EXISTS (SELECT 1 FROM [Security].[UserRoles] WHERE UserId = @UserId AND RoleId = @RoleId)
   BEGIN
       INSERT INTO [Security].[UserRoles] (UserId, RoleId)
       VALUES (@UserId, @RoleId)
   END
   ```

### 2. Seed Initial Data (Optional)

Create a console application or use Entity Framework migrations to seed:
- Default genres (Action, Comedy, Drama, etc.)
- Sample movies

### 3. Test API Endpoints

1. Open Swagger UI: `https://localhost:7085/swagger`
2. Test each endpoint:
   - GET /api/Genres
   - POST /api/Genres
   - GET /api/Movies
   - POST /api/Movies

---

## Troubleshooting

### Issue: "Connection string 'DefaultConnection' not found"

**Solution:**
- Verify `appsettings.json` exists and contains `ConnectionStrings` section
- Check file encoding (should be UTF-8)

### Issue: "Cannot open database"

**Solution:**
- Verify SQL Server is running
- Check connection string is correct
- Try creating database manually:
  ```sql
  CREATE DATABASE MyApp;
  ```

### Issue: "Migration not found"

**Solution:**
- List migrations:
  ```bash
  dotnet ef migrations list
  ```
- If no migrations, create one:
  ```bash
  dotnet ef migrations add InitialCreate
  dotnet ef database update
  ```

### Issue: "Port already in use"

**Solution:**
- Change port in `Properties/launchSettings.json`:
  ```json
  "applicationUrl": "https://localhost:7086;http://localhost:5170"
  ```
- Or kill the process using the port:
  ```bash
  # Windows
  netstat -ano | findstr :7085
  taskkill /PID <PID> /F
  ```

### Issue: "Email sending fails"

**Solution:**
- Verify email credentials are correct
- For Gmail, ensure App Password is used (not regular password)
- Check firewall/antivirus isn't blocking SMTP
- Verify SMTP server and port are correct

### Issue: "Swagger not showing"

**Solution:**
- Verify you're running in Development mode
- Check `appsettings.Development.json` exists
- Verify `ASPNETCORE_ENVIRONMENT` is set to `Development`

### Issue: "HTTPS certificate error"

**Solution:**
- Trust the development certificate:
  ```bash
  dotnet dev-certs https --trust
  ```
- Or use HTTP only (not recommended)

---

## Development Workflow

### Daily Development:

1. **Start Development Server:**
   ```bash
   dotnet run
   ```

2. **Make Code Changes:**
   - Edit files as needed
   - The server will auto-reload (hot reload)

3. **Create Database Migration (if needed):**
   ```bash
   dotnet ef migrations add MigrationName
   dotnet ef database update
   ```

4. **Test Changes:**
   - Use Swagger UI for API testing
   - Use browser for MVC testing
   - Check console for errors

### Before Committing:

1. **Build Project:**
   ```bash
   dotnet build
   ```

2. **Check for Warnings:**
   - Fix any compiler warnings

3. **Test Application:**
   - Test critical paths
   - Verify API endpoints

---

## Production Deployment

### Pre-Deployment Checklist:

- [ ] Update connection string for production database
- [ ] Move email credentials to secure storage (Azure Key Vault, etc.)
- [ ] Update CORS settings for production domain
- [ ] Configure HTTPS properly
- [ ] Set `ASPNETCORE_ENVIRONMENT` to `Production`
- [ ] Review and update logging configuration
- [ ] Test all functionality
- [ ] Review security settings
- [ ] Set up backup strategy for database

### Deployment Options:

1. **Azure App Service**
2. **IIS (Windows Server)**
3. **Docker Container**
4. **Linux with Nginx**

---

## Additional Resources

- [.NET Documentation](https://docs.microsoft.com/dotnet)
- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [SQL Server Documentation](https://docs.microsoft.com/sql)

---

## Getting Help

If you encounter issues:

1. Check this guide's Troubleshooting section
2. Review the main README.md
3. Check application logs
4. Review error messages carefully
5. Search for similar issues online
6. Check Entity Framework migration history

---

**Last Updated:** 2024

