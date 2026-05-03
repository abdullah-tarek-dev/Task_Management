📌 Task Management API (.NET Core)

A simple and scalable Task Management REST API built with ASP.NET Core Web API, featuring authentication, authorization, and clean architecture principles.

🚀 Features
👤 User Registration & Login (JWT Authentication)
🔐 Role-based Authorization (User / Admin)
🔄 Refresh Token support
📋 CRUD operations for Tasks
🔍 Filtering, Searching & Pagination
🧠 Clean Service + Repository architecture
⚠️ Global Exception Handling Middleware
🗄️ PostgreSQL Database integration (EF Core)
🏗️ Tech Stack
ASP.NET Core Web API
Entity Framework Core
PostgreSQL
JWT Authentication
C#
Swagger / OpenAPI
📁 Project Structure
Task_Management/
│
├── Controllers/
│   ├── AuthController.cs
│   └── TasksController.cs
│
├── Models/
│   ├── User.cs
│   ├── TaskItem.cs
│   └── RefreshToken.cs
│
├── DTOs/
│   ├── Auth/
│   └── Task/
│
├── Services/
│   ├── AuthService.cs
│   ├── TaskService.cs
│   └── PasswordService.cs
│
├── Data/
│   └── AppDbContext.cs
│
├── Middleware/
│   └── ExceptionMiddleware.cs
│
├── Auth/
│   ├── JwtTokenService.cs
│   └── TokenService.cs
│
└── Program.cs
⚙️ Setup Instructions
1️⃣ Clone the repo
git clone https://github.com/your-username/task-management-api.git
cd task-management-api
2️⃣ Configure Database

Update appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=your database name;Username=postgres;Password=your password"
}
3️⃣ Configure JWT
"Jwt": {
  "Key": "YOUR_SUPER_SECRET_KEY",
  "Issuer": "TaskManagementAPI",
  "Audience": "TaskManagementClient"
}
4️⃣ Run Migrations
dotnet ef migrations add InitialCreate
dotnet ef database update
5️⃣ Run Project
dotnet run
📌 API Endpoints
🔐 Auth
Method	Endpoint	Description
POST	/api/Auth/register	Register new user
POST	/api/Auth/login	Login user
POST	/api/Auth/refresh	Refresh JWT token
📋 Tasks
Method	Endpoint	Description
GET	/api/Tasks	Get all tasks
GET	/api/Tasks/{id}	Get task by id
POST	/api/Tasks	Create task
PUT	/api/Tasks/{id}	Update task
DELETE	/api/Tasks/{id}	Delete task (Admin only)
🔐 Authentication

Use JWT token in requests:

Authorization: Bearer YOUR_TOKEN
⚠️ Notes
Make sure PostgreSQL is running
Run migrations before starting
Refresh tokens stored in DB
Global exception middleware handles errors
👨‍💻 Author

Abdullah Tarek 
Backend Developer (.NET / Full Stack)

⭐ Future Improvements
Docker support
Unit Testing
Clean Architecture (Onion / CQRS)
Logging (Serilog)
Redis caching
