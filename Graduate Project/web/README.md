# Thriving Together web application

This directory contains the active web application. It uses Laravel 12 with server-rendered
Blade pages, SQLite for local development, and separate Flask services for the
Arabic and English pronunciation exercises.

## Requirements

- PHP 8.2 or later with PDO SQLite
- Composer
- Node.js 20 or later

## Setup

```powershell
composer install
copy .env.example .env
php artisan key:generate
New-Item -ItemType File database\database.sqlite -Force
php artisan migrate
npm install
npm run build
```

Start the application with:

```powershell
php artisan serve
```

The optional pronunciation service URLs are configured with
`ARABIC_PRONUNCIATION_URL` and `ENGLISH_PRONUNCIATION_URL`.
The service source is located at `../services/pronunciation`.

## Verification

```powershell
php artisan test
php vendor\bin\pint --test
composer audit
npm audit
npm run build
```

The application stores users in the legacy-compatible `yassdb` table and
contact form submissions in `contact_messages`.
