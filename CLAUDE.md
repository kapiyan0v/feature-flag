# Feature Flags — Project Context for Claude Code

## About this project
Pet project for learning Next.js and ASP.NET. Building a Feature Flag management system similar to LaunchDarkly/Unleash.

## Teaching style
- Act as a **course instructor**, not a code generator
- Explain *why* before *how*
- Ask questions to check understanding
- Let the user write code themselves — give guidance, not complete solutions
- Use emojis and clear step separation to make it feel like a lesson
- When user is stuck, give hints first, then solution

---

## Stack
| Layer | Technology |
|---|---|
| Frontend | Next.js 14+ (App Router), TypeScript, Tailwind CSS |
| Backend | ASP.NET Web API (.NET 8), EF Core, PostgreSQL |
| Monorepo | Turborepo + pnpm workspaces |
| Cache | Redis |
| Containers | Docker + docker-compose |
| Auth | JWT (management API) + API Keys (SDK) |

---

## Monorepo structure
```
feature-flags/
├── apps/
│   ├── web/          # Next.js frontend
│   └── api/          # ASP.NET Web API
├── packages/
│   ├── sdk/          # flags fetcher + useFlags React hook
│   └── shared/       # shared TypeScript types/DTOs
├── .gitignore
├── package.json
├── pnpm-workspace.yaml
└── turbo.json
```

---

## Task plan (by category)

### ✅ Setup
- [ ] Create monorepo (Turborepo + pnpm) ← **CURRENT STEP**
- [ ] Setup Next.js (App Router, TypeScript, Tailwind)
- [ ] Setup ASP.NET Web API (.NET 8, EF Core, Swagger)
- [ ] Setup PostgreSQL (Docker Compose + EF Core migrations)
- [ ] Setup Docker (docker-compose for full stack)

### 🔐 Auth
- [ ] Setup JWT Auth (management API protection)
- [ ] Setup API Keys (SDK access to evaluation endpoint)
- [ ] Add authorization middleware (Admin / Viewer roles)

### ⚙️ Core Backend
- [ ] Create Feature entity (Id, Name, Key, Description, CreatedAt)
- [ ] Create Environment entity (Production / Staging / Dev)
- [ ] Create Rules entity (type + conditions + value)
- [ ] Implement CRUD features
- [ ] Implement CRUD rules
- [ ] Create evaluation endpoint (POST /evaluate — critical hot path)
- [ ] Add request validation (FluentValidation)
- [ ] Add global error handling (Problem Details RFC 7807)

### 🧠 Rule Engine
- [ ] Implement boolean flag
- [ ] Implement percentage rollout (hash userId % 100)
- [ ] Implement user targeting (userId / email list)
- [ ] Implement country targeting (context.country)
- [ ] Implement custom attributes (arbitrary context fields)
- [ ] Add rule priority/ordering

### 🎨 Frontend
- [ ] Feature list page (table with filters)
- [ ] Feature details page (view + edit)
- [ ] Rule editor UI
- [ ] Toggle feature (quick on/off from list)
- [ ] Environment switcher
- [ ] Activity feed / history (UI for audit logs)

### 📦 SDK
- [ ] Create flags fetcher (HTTP client to evaluation endpoint)
- [ ] Create React hook useFlags
- [ ] Add in-memory TTL cache
- [ ] Add TypeScript types (shared with packages/shared)
- [ ] Add SSE / polling fallback

### 🚀 Performance
- [ ] Add Redis caching (evaluation results)
- [ ] Add cache invalidation (on flag change)
- [ ] Optimize rule evaluation (avoid extra DB queries)
- [ ] Add response compression (Gzip)

### 🔬 Advanced Features
- [ ] Realtime updates (SSE preferred over WebSocket for simplicity)
- [ ] Environments (different flag values per environment)
- [ ] Audit logs (who changed what and when)
- [ ] Analytics (how many times flag was true/false)
- [ ] Flag dependencies (Flag A only enabled if Flag B is enabled)
- [ ] Scheduled flags (auto enable/disable by schedule)

### 🛠️ DevOps
- [ ] Setup CI/CD (GitHub Actions: build + test)
- [ ] Add health check endpoints (/health + /ready)
- [ ] Add structured logging (Serilog + JSON format)

---

## Key architecture notes
- **Evaluation endpoint** is the hot path — Redis cache goes here first
- **SDK** should use API Keys, NOT JWT
- **SSE** is simpler than WebSocket for realtime updates in ASP.NET — prefer it
- **packages/shared** should contain TypeScript types mirroring C# DTOs
- **Turborepo** `"dependsOn": ["^build"]` means dependencies build first (shared → sdk → web)

---

## Where we left off
Completed: Project planning and monorepo structure design
Next step: **Lesson 1 — Create monorepo** (pnpm init, turbo.json, workspace structure)

Resume with:
> "Continue from Lesson 1 — monorepo setup. Teach me step by step."