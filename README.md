# LUME

<div align="center">

![ASP.NET](https://img.shields.io/badge/ASP.NET-Razor-512BD4?logo=dotnet)
![C#](https://img.shields.io/badge/C%23-.NET-239120?logo=c-sharp)
![TailwindCSS](https://img.shields.io/badge/TailwindCSS-3-38B2AC?logo=tailwind-css)
![UIkit](https://img.shields.io/badge/UIkit-3.25.19-2396F3)
![Status](https://img.shields.io/badge/status-frontend%20MVP%20in%20progress-yellow)

A social media web app for sharing moments through posts, stories, and reels built frontend-first as an ASP.NET Razor MVP, with the backend to follow.

• [Report Bug](https://github.com/abdhullah200/LUME/issues)

</div>

---

## 🎯 About LUME

**LUME** is a social media platform focused on visual sharing posts, stories, and reels. It's being built **frontend-first**: the UI and interaction layer are being fully designed and built out as a static/mock-data MVP using Razor views, Tailwind CSS, and UIkit before any backend logic is wired in.

The goal is to get the look, feel, and flow of the app right first, then layer in a real backend (auth, database, media storage, feeds) once the frontend experience is solid.

---

## ✨ Core Features (Frontend MVP)

### 📸 Posts
- Photo/video post cards in a scrollable feed
- Like, comment, and share UI
- Post detail view

### 🟣 Stories
- Story ring/avatar bar at the top of the feed
- Full-screen story viewer with progress bars
- Story upload UI

### 🎬 Reels
- Vertical, full-screen reel feed
- Swipe/scroll-based navigation
- Reel upload UI

### ⬆️ Upload
- Unified upload flow for posts, stories, and reels
- Media preview before posting


---

## 🏗️ Project Status

LUME is being built in two clear phases:

| Phase | Status |
|-------|--------|
| **1. Frontend MVP** (Razor views, Tailwind, UIkit, mock data) | 🚧 In Progress |
| **2. Backend** (database, auth, real data, media storage, APIs) | ⏳ Coming Soon |

The frontend is being built first and treated as the MVP full page layouts, components, and interactions for the feed, stories, reels, and upload flows before any backend work begins.

---

## 📦 Project Structure

```
LUME/
├── Pages/ or Views/        # Razor views for Feed, Stories, Reels, Upload, Profile
├── wwwroot/
│   ├── css/                # Tailwind CSS
│   └── js/                 # UIkit + custom JS
├── Components/             # Shared Razor partials/components
├── package.json            # Frontend tooling (UIkit)
└── README.md
```

*(Structure will expand as backend work begins.)*

---

## 🚀 Quick Start

### Prerequisites
- **.NET SDK** (for running the Razor app)
- **Node.js** (for Tailwind/UIkit tooling)

### Installation Steps

```bash
# 1. Clone the repository
git clone https://github.com/abdhullah200/LUME.git
cd LUME

# 2. Install frontend dependencies
npm install

# 3. Run the ASP.NET Razor app
dotnet run
```

Visit **https://localhost:5001** (or the port shown in your terminal) to view the app.

---

## 📦 Tech Stack

### Frontend (Current Focus)
| Technology | Purpose |
|-----------|---------|
| **ASP.NET Razor** | Server-rendered views for feed, stories, reels, upload |
| **Tailwind CSS** | Utility-first styling |
| **UIkit** | Component library for UI elements (modals, navs, etc.) |

### Backend (Coming Soon)
| Technology | Purpose |
|-----------|---------|
| **ASP.NET Core / C#** | Backend application logic |
| **SQL Server** | Data storage (users, posts, stories, reels) |
| **Media Storage** | TBD for uploaded photos/videos |
| **Authentication** | TBD user accounts and sessions |

---

## 🗺️ Roadmap

- [x] Feed page (posts) Razor + Tailwind + UIkit
- [ ] Stories bar + full-screen story viewer
- [ ] Reels vertical feed
- [ ] Upload flow (posts, stories, reels)
- [ ] Profile page
- [ ] Backend: database schema
- [ ] Backend: authentication
- [ ] Backend: media upload/storage
- [ ] Backend: real feed data wired to frontend

---

<div align="center">

### 💜 Made with Love and Code by Abdullah Ariff

**If you found this project helpful, please consider giving it a ⭐ on GitHub!**

[![GitHub Stars](https://img.shields.io/github/stars/abdhullah200/LUME?style=social)](https://github.com/abdhullah200/LUME)
[![GitHub Forks](https://img.shields.io/github/forks/abdhullah200/LUME?style=social)](https://github.com/abdhullah200/LUME/fork)
[![GitHub Issues](https://img.shields.io/github/issues/abdhullah200/LUME)](https://github.com/abdhullah200/LUME/issues)

</div>

<div align="center">
Made with ❤️ using ASP.NET Razor, Tailwind CSS, and UIkit
</div>
