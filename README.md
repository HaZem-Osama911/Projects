# Projects

A collection of my web applications, APIs, and programming practice, with a focus on **C# and ASP.NET Core**.

## Start here

### Thriving Together — Speech & Communication
An Arabic-first graduation project with educational activities, user accounts, and separate Arabic and English pronunciation services powered by Whisper.

**Stack:** Laravel 12 · Blade · SQLite · Python · Flask · Whisper

[Project guide & setup](Graduate%20Project/README.md) · [Web application](Graduate%20Project/web) · [Pronunciation services](Graduate%20Project/services/pronunciation)

### Zedny — Education Platform
An education platform with courses, enrollments, student–teacher relationships, and separate administration and teaching workflows.

**Stack:** ASP.NET Core MVC · Entity Framework Core · SQL Server · Identity

[Source](zedny/EduPlatform) · [Project guide](zedny/README.md)

### MegaSoft — Teams & Tasks
A team and task management application with team membership, task assignment, and a controller–service–repository structure.

**Stack:** ASP.NET Core MVC · Entity Framework Core · SQL Server · Identity

[Source](MegaSoft/MegaSoft) · [Project guide](MegaSoft/read.md)

### MyApp — Movies & User Management
A web application combining MVC pages and API endpoints for movies and genres, alongside user and role management.

**Stack:** ASP.NET Core MVC + Web API · Entity Framework Core · Identity · Swagger

[Source](MyApp/MyApp) · [Overview](MyApp/MyApp/README.md) · [Setup](MyApp/MyApp/SETUP_GUIDE.md) · [Architecture](MyApp/MyApp/ARCHITECTURE.md) · [API examples](MyApp/MyApp/API_EXAMPLES.md)

## Repository directory

| Folder | Contents |
| --- | --- |
| [zedny](zedny) | Education platform; featured above. |
| [MegaSoft](MegaSoft) | Team and task management; featured above. |
| [MyApp](MyApp) | MVC and API application; featured above. |
| [MoviesApi](MoviesApi) | Movie and genre API, including CRUD operations, genre filtering, and poster validation. |
| [ECommereceAPI](ECommereceAPI) | E-commerce API project with product and user models. |
| [SupermarketAPI](SupermarketAPI) | Supermarket API project. |
| [Bank System](Bank%20System) | C# console project. |
| [HRSystem](HRSystem) | C# console project. |
| [Graduate Project](Graduate%20Project) | Thriving Together: Laravel web application, educational activities, and Arabic/English Flask + Whisper pronunciation services. See its README for setup and Git LFS media downloads. |
| [Web API](Web%20API) | API project with student, administrator, authentication, and AI controllers. |
| [Ai_Tic_Tac](Ai_Tic_Tac) | Packaged application files and an additional MyApp source folder. |
| [Simple Random (S,N)](Simple%20Random%20%28S%2CN%29) | Additional programming project files. |

## Running a project

Each folder is a separate project; there is no single startup application for this repository. For Thriving Together, follow its [Laravel/Python setup guide](Graduate%20Project/README.md). The steps below apply to the .NET projects.

1. Choose a project and read its linked guide where available.
2. Check its `.csproj` file for the required .NET SDK version.
3. Configure the database and any local service settings for that project.
4. Restore dependencies, apply the project's database migrations where required, and launch it from its project directory.

The collection includes different .NET targets: for example, `MoviesApi` targets .NET 6, `ECommereceAPI` targets .NET 9, and the three featured web applications target .NET 10.

## More work

[C++ problem-solving practice](https://github.com/HaZem-Osama911/Problem-Solving) · [GitHub profile](https://github.com/HaZem-Osama911)
