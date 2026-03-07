# Phase 9 — Polish & Deploy

## Priority: MEDIUM
## Status: Done (polish complete, deploy deferred)

## Overview
Final polish, responsive testing, error handling, and deploy to Railway.

## Polish Tasks
1. Error pages (404, 500) — styled with DaisyUI
2. Loading states (HTMX indicators)
3. Toast notifications (Alpine.js) for add to cart, order placed, etc.
4. Form validation (client + server)
5. SEO basics (title, meta description per page)
6. Image lazy loading
7. Responsive check: 375px, 768px, 1024px, 1440px
8. prefers-reduced-motion for animations
9. Accessibility: alt text, focus states, aria-labels

## Deploy Steps
1. Create Railway account
2. Add PostgreSQL addon (or use Neon connection string)
3. Create Dockerfile for ASP.NET Core 8
4. Push to GitHub → Railway auto-deploy
5. Set environment variables (connection string, etc.)
6. Verify live site works
7. Custom domain (optional)

## Todo
- [ ] Error pages (404, 500)
- [ ] Toast notifications
- [ ] Form validation
- [ ] Responsive testing
- [ ] Accessibility check
- [ ] Create Dockerfile
- [ ] Deploy to Railway
- [ ] Verify live site

## Success Criteria
- No broken pages or errors
- Responsive on all breakpoints
- Deploy successful, live URL works
- All features functional on production
