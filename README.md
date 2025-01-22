# Hospital Report Management System

This project is a **Hospital Report Management System** built using **ASP.NET Core API**. It allows hospitals to generate and manage patient reports, send SMS notifications to patients, and store all data securely.

## Features

- **Patient Report Management**: Hospitals can create, update, and manage patient reports.
- **SMS Notifications**: Patients receive an SMS with their report code using **Twilio**.
- **User Authentication**: Secure user authentication and authorization using **ASP.NET Core Identity**.
- **Clean Architecture**: The project follows the **Onion Architecture** for better separation of concerns.
- **Design Patterns**: Uses **Repository Pattern** and **Unit of Work** for efficient data access and management.

## Technologies Used

- **Backend**: ASP.NET Core API
- **Database**: SQL Server (or any other supported database)
- **SMS Service**: Twilio
- **Authentication**: ASP.NET Core Identity
- **Design Patterns**: Repository Pattern, Unit of Work
- **Architecture**: Onion Architecture

## How It Works

1. **Create a Report**:
   - Hospitals can create a report for a patient by entering their details, report description, and the number of leave days.
   - The report is saved in the database.

2. **Send SMS**:
   - Once the report is saved, the system sends an SMS to the patient's phone number using **Twilio**.
   - The SMS contains a unique report code that the patient can use to access their report.

3. **User Authentication**:
   - The system uses **ASP.NET Core Identity** for secure user authentication and role-based authorization.
   - Only authorized users (e.g., hospital staff) can create or manage reports.

## Project Structure

- **Core**: Contains the domain models and business logic.
- **Infrastructure**: Handles data access, repositories, and external services (e.g., Twilio).
- **API**: The main API layer that exposes endpoints for report management and user authentication.
- **Tests**: Unit and integration tests for the application.

## Setup Instructions

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/hospital-report-system.git

   
2. Update the database connection string in appsettings.json.
3. Run migrations to create the database:
   dotnet ef database update

4. Configure Twilio credentials in appsettings.json:
   "Twilio": {
  "AccountSid": "your_account_sid",
  "AuthToken": "your_auth_token",
  "PhoneNumber": "your_twilio_phone_number"
}

5. Run the application:
   dotnet run
