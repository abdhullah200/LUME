# LUME
# LUME

<div align="center">

![ASP.NET](https://img.shields.io/badge/ASP.NET-Core%20MVC-512BD4?logo=dotnet)
![C#](https://img.shields.io/badge/C%23-.NET%208-239120?logo=c-sharp)
![EF Core](https://img.shields.io/badge/EF%20Core-8.0.21-512BD4?logo=dotnet)
![SQL Server](https://img.shields.io/badge/SQL%20Server-LocalDB-CC2927?logo=microsoft-sql-server)
![TailwindCSS](https://img.shields.io/badge/TailwindCSS-3-38B2AC?logo=tailwind-css)
![UIkit](https://img.shields.io/badge/UIkit-3.25.19-2396F3)
![Status](https://img.shields.io/badge/status-in%20development-orange)

A social media web application for sharing posts and discovering visual content, built with ASP.NET Core MVC, Razor Views, Entity Framework Core, SQL Server, Tailwind CSS, and UIkit.

• [Report a Bug](https://github.com/abdhullah200/LUME/issues)

</div>

---

## 🎯 About LUME

**LUME** is a social media experience centered around posts, comments, likes, saved posts, profiles, and visual content. The application began as a frontend-first interface and now includes a SQL Server data layer for the main feed and profile functionality.

The current solution is still in development. Authentication screens, stories, and some post-menu actions are present as UI or placeholders, while the active user is currently represented by the seeded user with `UserId = 1`.

---

## ✨ Features

### 📸 Posts and Feed
- Loads posts from SQL Server through EF Core and orders them by creation date
- Creates posts with text and optional image uploads
- Stores uploaded images locally under `Lume/wwwroot/images/Uploaded`
- Supports persistent like/unlike and save/unsave actions
- Supports adding and removing comments
- Supports toggling a user's post between public and private
- Displays relative timestamps and post interaction counts

### 👤 Profiles
- Profile page for the seeded user or a requested user ID
- Displays profile information and the user's posts
- Shows post count and profile presentation components
- Includes UI placeholders for future follower, following, bio, and story-highlight data

### 🟣 Stories and 🎬 Reels
- Story bar, story creation modal, and story presentation UI are included
- Stories do not yet have database persistence or a complete backend flow
- Reels are planned but are not implemented yet

### 🧭 Layout and Navigation
- Shared responsive layout with sidebar and topbar navigation
- Follower suggestions and “Trends for You” sections
- Reusable Razor partials for posts, profiles, navigation, and modals

### 🔐 Authentication UI
- Dedicated `LumeAuth` project with login and sign-up Razor Views
- Form validation and local return-URL sanitization are included
- Credential verification, password storage, identity, and authentication cookies are not connected yet

---

## 🏗️ Current Status

| Area | Status |
|------|--------|
| **Posts and feed** — EF Core queries and create flow | ✅ Implemented |
| **Likes and saved posts** — database-backed toggles | ✅ Implemented |
| **Comments** — add and remove flows | ✅ Implemented |
| **Post visibility** — public/private toggle | ✅ Implemented |
| **Profile page** — profile and post grid | ✅ Implemented |
| **Layout and navigation** | ✅ Implemented |
| **Local image uploads** | ✅ Implemented |
| **Stories** | 🚧 UI only |
| **Authentication** | 🚧 UI only |
| **Reels** | ⏳ Planned |
| **Cloud media storage** | ⏳ Planned |

---

## 📦 Project Structure

```
LUME/
├── Lume/                              # Main ASP.NET Core MVC application
│   ├── Controllers/
│   │   ├── HomeController.cs          # Feed, posts, likes, favorites, comments
│   │   └── ProfileController.cs       # Profile and post grid
│   ├── ViewModels/                    # Post, comment, favorite, visibility, and profile VMs
│   ├── Views/                         # Razor Views and shared partials
│   │   ├── Home/                      # Feed page
│   │   ├── Profile/                   # Profile page
│   │   └── Shared/                    # Layout, navigation, posts, profiles, and modals
│   ├── wwwroot/                       # CSS, JavaScript, static assets, and local uploads
│   ├── Migrations/                    # Application migration files
│   ├── Program.cs                     # Service registration and HTTP pipeline
│   └── appsettings.json               # Logging and database configuration
├── LumeAuth/                          # Authentication UI project
│   ├── Areas/Auth/Views/              # Login and sign-up Razor Views
│   ├── Controllers/                   # AccountController
│   └── ViewModels/                    # Login, sign-up, and validation models
├── LumeData/                          # Shared data access class library
│   ├── Models/                        # User, Post, Like, Comment, and Favorite entities
│   ├── Helpers/                       # Development database seeding
│   ├── Migrations/                    # EF Core schema migrations
│   └── AppDbContext.cs                # Database context and relationships
├── Lume.slnx                          # Solution file
├── package.json                       # UIkit dependency
└── README.md
```

---

## 🚀 Quick Start

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server LocalDB](https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb)
- Node.js and npm are optional unless you need to manage the UIkit dependency

### Run the application

```bash
# Clone the repository
git clone https://github.com/abdhullah200/LUME.git
cd LUME

# Restore .NET dependencies
dotnet restore

# Optional: install the UIkit package declared in package.json
npm install

# Start the web application
dotnet run --project Lume
```

Open the HTTPS URL printed in the terminal. The port can vary by local launch configuration; do not assume it is always `https://localhost:5001`.

### Database setup

On startup, the application:

1. Reads the SQL Server connection string from `Lume/appsettings.json`.
2. Applies pending EF Core migrations with `Database.MigrateAsync()`.
3. Seeds an initial user and sample posts when the database is empty.

The default connection string uses SQL Server LocalDB and the existing configuration key is `ConnectionStrings:Defualt`:

```text
Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LumeDb;Integrated Security=True;Pooling=False;Encrypt=False;Trust Server Certificate=True
```

To use another SQL Server instance, replace the value of `ConnectionStrings:Defualt` in `Lume/appsettings.json` or override it with environment-specific configuration. The current seed logic is intended for development and should be reviewed before production use.

---

## 🧰 Tech Stack

| Technology | Usage |
|-----------|-------|
| **ASP.NET Core MVC (.NET 8)** | Controllers, routing, and application pipeline |
| **Razor Views** | Server-rendered feed, profiles, authentication UI, and shared partials |
| **Entity Framework Core 8.0.21** | ORM and database access |
| **SQL Server / LocalDB** | Application data store |
| **EF Core Migrations** | Database schema management and startup migration application |
| **Tailwind CSS** | Utility-first styling and generated application styles |
| **UIkit 3.25.19** | UI components such as modals, dropdowns, and navigation elements |
| **C#** | Application and data-layer implementation |

---

## 🗺️ Roadmap

- [x] Feed page with database-backed posts
- [x] Create post flow with local image upload
- [x] Persistent likes and saved posts
- [x] Comments and comment removal
- [x] Post visibility toggle
- [x] Profile page and post grid
- [x] Shared sidebar, topbar, trends, and follower suggestions UI
- [ ] Connect login and sign-up to real authentication and password hashing
- [ ] Replace hardcoded `UserId = 1` with the authenticated user
- [ ] Persist stories and complete the story viewer flow
- [ ] Build the reels feed and upload flow
- [ ] Add production-ready media storage
- [ ] Add authorization and ownership checks for all write operations

---

<div align="center">

### 💜 Made with Love and Code by Abdullah Ariff

**If you found this project helpful, please consider giving it a ⭐ on GitHub!**

[![GitHub Stars](https://img.shields.io/github/stars/abdhullah200/LUME?style=social)](https://github.com/abdhullah200/LUME)
[![GitHub Forks](https://img.shields.io/github/forks/abdhullah200/LUME?style=social)](https://github.com/abdhullah200/LUME/fork)
[![GitHub Issues](https://img.shields.io/github/issues/abdhullah200/LUME)](https://github.com/abdhullah200/LUME/issues)

</div>

<div align="center">
Made with ❤️ using ASP.NET Core, EF Core, Tailwind CSS, and UIkit
</div>
