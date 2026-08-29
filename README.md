# LUME

<div align="center">

![ASP.NET](https://img.shields.io/badge/ASP.NET-MVC-512BD4?logo=dotnet)
![C#](https://img.shields.io/badge/C%23-.NET%208-239120?logo=c-sharp)
![EF Core](https://img.shields.io/badge/EF%20Core-8-512BD4?logo=dotnet)
![SQL Server](https://img.shields.io/badge/SQL%20Server-LocalDB-CC2927?logo=microsoft-sql-server)
![TailwindCSS](https://img.shields.io/badge/TailwindCSS-3-38B2AC?logo=tailwind-css)
![UIkit](https://img.shields.io/badge/UIkit-3.25.19-2396F3)
![Status](https://img.shields.io/badge/status-backend%20wired%20up-brightgreen)

A social media web app for sharing moments through posts, stories, and reels, built as an ASP.NET Core MVC app with a real EF Core + SQL Server backend now wired in behind the Razor/Tailwind/UIkit frontend.

• [Report Bug](https://github.com/abdhullah200/LUME/issues)

</div>

---

## 🎯 About LUME

**LUME** is a social media platform focused on visual sharing posts, stories, and reels. It started as a frontend-first mock-data MVP and has since grown a real data layer: posts and likes are now backed by SQL Server via EF Core, with a dedicated **LumeData** project holding the models, `DbContext`, and migrations.

Stories and reels are still frontend-only UI (mock data) while the posts feed is now fully wired to the database.

---

## ✨ Core Features

### 📸 Posts (backend-wired)
- Feed reads live post data from SQL Server, ordered by most recent
- Create post with optional image upload (saved to `wwwroot/images/Uploaded`)
- Like/unlike toggling persisted via the `Likes` table
- Relative post timestamps ("Just now", "5m ago", "2h ago", "3d ago")
- Post options menu (Report / Set as Private / Delete Post — UI in place)

### 🟣 Stories (frontend UI)
- Story ring/avatar bar at the top of the feed
- Story creation modal
- Full-screen story viewer/upload flow — UI only, mock data for now

### 🎬 Reels (planned)
- Vertical, full-screen reel feed
- Swipe/scroll-based navigation
- Reel upload UI

### 🧭 Navigation & Layout
- Sidebar and topbar navigation partials
- Follower suggestions and "Trends for You" sidebar sections

### ⬆️ Upload
- Create Status modal for posting content + images
- Unified media preview before posting

---

## 🏗️ Project Status

| Area | Status |
|------|--------|
| **Feed (Posts + Likes)** — EF Core, SQL Server, real CRUD | ✅ Wired up |
| **Layout & Navigation** (Sidebar, Topbar, Feed shell) | ✅ In place |
| **Stories** (Razor views, Tailwind, UIkit, mock data) | 🚧 Frontend only |
| **Reels** | ⏳ Not started |
| **Authentication / real logged-in user** | ⏳ Not started (currently hardcoded `UserId = 1`) |
| **Comments** | ⏳ Not started |
| **Media storage (cloud)** | ⏳ Not started (local `wwwroot` uploads for now) |

---

## 📦 Project Structure

```
LUME/
├── Lume/                      # ASP.NET Core MVC web app
│   ├── Controllers/           # HomeController (feed, create post, toggle like)
│   ├── ViewModels/            # PostVM, PostLike, etc.
│   ├── Views/
│   │   ├── Home/              # Index (feed)
│   │   └── Shared/
│   │       ├── Home/          # _Post partial
│   │       ├── Modals/        # _CreateStatus, _CreateStory
│   │       ├── Navigation/    # _Sidebar, _Topbar
│   │       └── SideBar/       # FollowerSuggestions, _TrendsForYouSection
│   ├── wwwroot/                # Tailwind CSS, UIkit, static assets, uploaded images
│   ├── Program.cs              # App startup, DB migration + seeding on boot
│   └── appsettings.json        # Connection strings, logging config
├── LumeData/                   # Data access layer (class library)
│   ├── Models/                 # User, Post, Like entities
│   ├── Helpers/                # DbInitializer (dev seed data)
│   ├── Migrations/             # EF Core migrations
│   └── AppDbContext.cs
├── Lume.slnx                   # Solution file
├── package.json                 # Frontend tooling (UIkit)
└── README.md
```

---

## 🚀 Quick Start

### Prerequisites
- **.NET 8 SDK**
- **SQL Server LocalDB** (or update the connection string for your own SQL Server instance)
- **Node.js** (for Tailwind/UIkit tooling)

### Installation Steps

```bash
# 1. Clone the repository
git clone https://github.com/abdhullah200/LUME.git
cd LUME

# 2. Install frontend dependencies
npm install

# 3. Restore .NET dependencies
dotnet restore

# 4. Run the app (migrations apply and dev seed data loads automatically on startup)
dotnet run --project Lume
```

Visit **https://localhost:5001** (or the port shown in your terminal) to view the app.

> The default connection string in `appsettings.json` points at `(localdb)\MSSQLLocalDB`. Update `ConnectionStrings:Defualt` if you're using a different SQL Server instance.

---

## 📦 Tech Stack

### Frontend
| Technology | Purpose |
|-----------|---------|
| **ASP.NET Core Razor Views** | Server-rendered feed, stories, modals, navigation |
| **Tailwind CSS** | Utility-first styling |
| **UIkit** | Component library for UI elements (modals, sliders, dropdowns) |

### Backend
| Technology | Purpose |
|-----------|---------|
| **ASP.NET Core MVC (.NET 8)** | Controllers and application logic |
| **Entity Framework Core 8** | ORM / data access (`LumeData` project) |
| **SQL Server (LocalDB)** | Data storage for users, posts, likes |
| **EF Core Migrations** | Schema management, applied automatically on startup |

---

## 🗺️ Roadmap

- [x] Feed page (posts) — Razor + Tailwind + UIkit
- [x] EF Core + SQL Server backend for posts and likes
- [x] Create post flow with image upload
- [x] Like/unlike toggle persisted to the database
- [x] Sidebar, topbar, and trends/follower suggestions UI
- [ ] Comments
- [ ] Stories backend (upload/story data persisted)
- [ ] Authentication (replace hardcoded logged-in user)
- [ ] Profile page
- [ ] Cloud media storage for uploads

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
