# Resource Booking Application

Full-stack application for managing resources and bookings.

## Tech Stack

- ASP.NET Core / .NET 10
- Entity Framework Core
- SQL Server
- React + TypeScript
- Tailwind CSS
- TanStack Query
- TanStack Router
- Docker / Docker Compose

## Features

### Backend

- Swagger
- CRUD for Resource and Booking
- Booking conflict validation
- Concurrent booking protection using SQL Server `Serializable` transaction
- Booking filtering by resource
- Validation that booking starts from the current UTC time
- I decided to set reservations directly to "Confirmed" instead of "Pending" as validating them would require an "Admin" role or an automated check system

### Frontend

- Resource list
- Resource CRUD page
- Booking creation form
- Booking list
- Booking cancellation
- Displaying booking errors
- TanStack Query for server state management
- TanStack Router for navigation
- Tailwind CSS

## Running the application

Requirements:

- Docker
- Docker Compose

Start the entire application with:

```bash
docker compose up --build

To view the complete API interactive documentation and test the endpoints, follow this link:
- http://localhost:5000/swagger/index.html (after the project launches)