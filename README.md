# SCE-Cinema Website

![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)
![GitHub last commit](https://img.shields.io/github/last-commit/TakNud/SCE--Cinema)
![GitHub issues](https://img.shields.io/github/issues/TakNud/SCE--Cinema)
![GitHub pull requests](https://img.shields.io/github/issues-pr/TakNud/SCE--Cinema)

This is a website for a small local cinema to allow customers to book movie tickets online.

## Features

- Browse movies and showtimes
- View information about each movie (plot, cast, runtime, etc.)
- Select seats when booking tickets
- Integrated payment processing with PayPal
- Ability to create a user account
- Account dashboard to view booking history and manage upcoming bookings
- Admin portal to manage movies, showtimes, and process refunds

## Tech Stack

<p align="center">
  <img src="https://raw.githubusercontent.com/github/explore/80688e429a7d4ef2fca1e82350fe8e3517d3494d/topics/aspnet/aspnet.png" alt="ASP.NET" height="50">
  <img src="https://raw.githubusercontent.com/github/explore/80688e429a7d4ef2fca1e82350fe8e3517d3494d/topics/css/css.png" alt="CSS" height="50">
  <img src="https://raw.githubusercontent.com/github/explore/80688e429a7d4ef2fca1e82350fe8e3517d3494d/topics/html/html.png" alt="HTML" height="50">
  <img src="https://raw.githubusercontent.com/github/explore/80688e429a7d4ef2fca1e82350fe8e3517d3494d/topics/javascript/javascript.png" alt="Javascript" height="50">
  <img src="https://raw.githubusercontent.com/github/explore/2d218e3aa252dc90eef269b34eeec1fbd15dc07e/topics/sqlite/sqlite.png" alt="SQLite" height="50">
</p>

## Frontend

The frontend is built with:

- HTML for markup
- CSS for styling
- JavaScript for interactivity
- Responsive design for mobile and desktop

## Backend

The backend uses:

- ASP.NET Core web API
- Entity Framework Core to access SQLite database
- Repository pattern for data access
- JWT authentication for account logins

## Database

Data is stored in a SQLite database with tables for:

- Movies
- Showtimes
- Ticket bookings
- Seats
- Users and roles

## Testing

- Unit tests on core logic and models with xUnit
- Integration testing API routes
- Manual E2E testing

## Getting Started

To run this project locally:

1. Clone this repo
2. Configure `appsettings.json`
3. Run database migrations to set up the SQLite database
4. Build and run the project
5. View homepage at `localhost:5000`

Payments will be in test mode unless you provide your own PayPal credentials.

## Next Steps

Future improvements:

- Integrate a mailing system to send booking confirmations/reminders
- Build out admin analytical dashboard
- Add social authentication options


