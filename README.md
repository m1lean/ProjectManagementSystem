# Project Management System

A web application for managing projects, tasks, and team members built with ASP.NET Core MVC.

## 📋 About the Project

The Project Management System provides a comprehensive interface for creating and managing projects, tasks, and team members. The application is developed using the MVC pattern and modern .NET technologies.

### ✨ Key Features

- **Project Management**: Create, edit, delete, and view projects
- **Project Filtering**: Filter by status (Open, In Progress, Closed)
- **Task Management**: Full CRUD operations for tasks linked to projects
- **Task Assignment**: Assign tasks to team members
- **Member Management**: Add and remove members from projects
- **Detailed Views**: View projects with associated tasks and members

## 🛠 Tech Stack

- **Backend**: ASP.NET Core MVC (.NET 9.0)
- **Language**: C# 13.0 with nullable enable
- **ORM**: Entity Framework Core
- **Database**: In-Memory Database (for development)
- **UI**: Razor Views + HTML/CSS
- **IDE**: JetBrains Rider (recommended)

### 📦 Dependencies (NuGet Packages)

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.8" />
<PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="9.0.8" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="9.0.8" />
```

## 🏗 Project Architecture

```
ProjectManagementSystem/
├── Controllers/           # MVC Controllers
│   ├── ProjectsController.cs
│   ├── TasksController.cs
│   ├── UsersController.cs
│   └── HomeController.cs
├── Models/               # Data Models
│   ├── Project.cs
│   ├── ProjectTask.cs
│   ├── User.cs
│   └── ProjectUser.cs
├── Data/                 # Database Context
│   └── ApplicationDbContext.cs
├── Services/             # Business Logic
│   ├── Interfaces/
│   │   ├── IProjectService.cs
│   │   ├── IProjectTaskService.cs
│   │   └── IUserService.cs
│   └── Implementations/
│       ├── ProjectService.cs
│       ├── ProjectTaskService.cs
│       └── UserService.cs
├── Views/                # Razor Views
│   ├── Projects/
│   ├── Tasks/
│   ├── Users/
│   └── Shared/
└── Program.cs           # Entry Point
```

## 🚀 Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [JetBrains Rider](https://www.jetbrains.com/rider/) or Visual Studio 2022

### Installation and Running

1. **Clone the repository**
   ```bash
   git clone https://github.com/m1lean/ProjectManagementSystem.git
   cd ProjectManagementSystem
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Run the application**
   ```bash
   dotnet run
   ```

4. **Open in browser**
   ```
   http://localhost:5000
   ```

### Running in JetBrains Rider

1. Open the project in Rider
2. Build the project (Ctrl+Shift+B)
3. Run the project (Ctrl+F5)

## 📊 Data Model

### Core Entities

- **Project** - Project with name, description, and status
- **ProjectTask** - Task linked to a project and assigned to a user
- **User** - System user
- **ProjectUser** - Junction entity for project members (many-to-many)

### Relationships

- Project → Tasks (one-to-many)
- Project ↔ Members (many-to-many through ProjectUser)
- Task → User (many-to-one for assignment)

## 🌐 API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/Projects` | List all projects |
| GET | `/Projects?status=Open` | Filter by status |
| GET | `/Projects/Details/{id}` | Project details |
| GET | `/Tasks?projectId={id}` | Project tasks |
| GET | `/Users` | List users |
| GET | `/Users/Participants?projectId={id}` | Project members |

## 🧪 Test Data

The application automatically creates test data on startup:
- Sample users
- Demo project
- Related tasks and members

## 🔧 Configuration

### Database Switch

To use a real database (e.g., SQL Server), modify in `Program.cs`:

```csharp
// Replace
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("ProjectManagementDb"));

// With
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
```

## 🚧 Current Limitations

- **Database**: Uses in-memory database, data is not persisted after restart
- **Authentication**: No authorization system
- **Validation**: Basic server-side validation only
- **UI**: Minimal interface without JavaScript

## 🎯 Roadmap

- [ ] Migrate to real database (SQL Server/PostgreSQL)
- [ ] Add authentication system (ASP.NET Core Identity)
- [ ] Improve UI with Bootstrap/modern CSS frameworks
- [ ] Add REST API endpoints
- [ ] Implement real-time notifications (SignalR)
- [ ] Add file attachments to tasks
- [ ] Role-based access control
- [ ] Mobile responsiveness
- [ ] Unit and integration tests

---

⭐ Don't forget to give the project a star if you found it helpful!