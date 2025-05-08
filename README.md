# Teach Hub

An **ASP.NET** project designed to provide a platform for managing online courses. This platform supports features such as course creation, user enrollment, and role-based access.

## Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [License](#license)

---

## Features

- Role-based access control for users (e.g., students, instructors, admins).
- Management of courses, users, and enrollments.
- Integration with SQL Server for database operations.
- User-friendly interface powered by Bootstrap and jQuery.
- Session management for user authentication and authorization.

---

## Technologies Used

- **Frameworks & Languages**:
  - ASP.NET
  - C#
  - HTML/CSS
  - JavaScript (jQuery)

- **Database**:
  - Microsoft SQL Server

- **Tools**:
  - Entity Framework Core
  - Bootstrap for styling
  - Docker for containerization

---

## Getting Started

### Prerequisites

1. .NET SDK 7.0 or later
2. Microsoft SQL Server
3. Docker (optional, for containerized deployment)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/Ali-Afifi/TeachHub.git
   ```

2. Navigate to the project directory:
   ```bash
   cd online-course-platform
   ```

3. Configure your database connection string in:
   - `appsettings.json`
   - `appsettings.Development.json`

4. Setup the database:
   ```sql
   -- Run the provided init.sql script to initialize the database schema.
   ```

5. Build and run the project:
   ```bash
   dotnet build
   dotnet run
   ```

### Docker Deployment

```bash
docker build -t online-course-platform .
docker run -p 5000:5000 online-course-platform
```

---

## Usage

- Access the platform at **`http://localhost:5000`** after running the application.
- Login as an admin to manage courses and users.

### Example Endpoints

- Home Page: `/`
- Privacy Policy: `/Home/Privacy`

---

## Project Structure

```
TeachHub/
├── Controllers/
│   ├── CoursesController.cs
│   ├── StudentsController.cs
│   ├── InstructorsController.cs
│   └── UsersController.cs
├── Data/
│   └── OnlineCoursesContext.cs
├── Models/
│   ├── User.cs
│   ├── Role.cs
│   └── Enrolled.cs
├── Views/
│   ├── Shared/
│   │   └── _Layout.cshtml
│   └── Courses/
├── wwwroot/
│   └── lib/
├── appsettings.json
├── init.sql
└── Program.cs
```

---

## License

The project is licensed under the MIT License. See `LICENSE` files in the `/wwwroot/lib` directory for more details on third-party dependencies.

---
