# Phase 1 — Setup Environment

## Priority: HIGH
## Status: Pending

## Overview
Install .NET 8 SDK, create ASP.NET Core MVC project, configure Tailwind + DaisyUI + Alpine.js + HTMX, connect PostgreSQL via EF Core.

## Requirements
- .NET 8 SDK installed
- VS Code + C# Dev Kit extension
- PostgreSQL database on Neon
- Tailwind CSS + DaisyUI configured
- Alpine.js + HTMX via CDN
- Be Vietnam Pro font loaded
- Heroicons available
- EF Core connected to PostgreSQL
- ASP.NET Core Identity scaffolded

## Implementation Steps

1. Install .NET 8 SDK from dotnet.microsoft.com
2. Install VS Code extensions: C# Dev Kit, C# Extensions
3. Create project:
   ```bash
   dotnet new mvc -n Nexora
   cd Nexora
   ```
4. Add NuGet packages:
   ```bash
   dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
   dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
   dotnet add package Microsoft.EntityFrameworkCore.Tools
   dotnet add package CloudinaryDotNet
   ```
<!-- Updated: Validation Session 1 - Added CloudinaryDotNet for image hosting -->
5. Create Neon PostgreSQL database, get connection string
6. Configure `appsettings.json` with connection string
7. Setup Tailwind CSS:
   ```bash
   npm init -y
   npm install -D tailwindcss @tailwindcss/cli
   npx tailwindcss init
   ```
8. Install DaisyUI: `npm install -D daisyui`
9. Configure `tailwind.config.js` (content paths for .cshtml, DaisyUI plugin, custom theme)
10. Add to `_Layout.cshtml`:
    - Tailwind compiled CSS
    - Be Vietnam Pro (Google Fonts)
    - Alpine.js (CDN)
    - HTMX (CDN)
    - Heroicons (inline SVG or icon component)
11. Configure `Program.cs`: DbContext, Identity, services
12. Create initial migration, verify DB connection
13. Run `dotnet watch run`, verify hot reload works

## Todo
- [ ] Install .NET 8 SDK
- [ ] Create MVC project
- [ ] Add NuGet packages (EF Core, Npgsql, Identity)
- [ ] Setup Neon PostgreSQL + connection string
- [ ] Setup Cloudinary account + config (CloudName, ApiKey, ApiSecret)
- [ ] Setup Tailwind CSS + DaisyUI
- [ ] Add CDN scripts (Alpine.js, HTMX, Be Vietnam Pro, Heroicons)
- [ ] Configure Program.cs (DbContext, Identity, DI)
- [ ] Run initial migration
- [ ] Verify project runs with hot reload

## Success Criteria
- `dotnet watch run` starts without errors
- Homepage renders with Tailwind + DaisyUI styling
- Be Vietnam Pro font visible
- Alpine.js reactive (`x-data` works)
- HTMX requests work (`hx-get` returns partial)
- Database connected (migration applied)
