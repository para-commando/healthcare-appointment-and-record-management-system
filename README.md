# 🏥 HealthCare-appointment-and-record-management-system

---

## 🌟 Project Overview

The **HealthCare-appointment-and-record-management-system** is a **comprehensive**, **scalable**, and **modular** application developed using **ASP.NET Core**. It facilitates efficient hospital management by providing robust features for managing patients, staff, appointments, prescriptions, and clinical data. The system integrates advanced technologies like **JWT-based authentication**, **Redis caching**, and **dynamic PDF generation**.

---

## ✨ Features

### 🔒 Authentication Management
- **Login and Registration APIs:** Secure authentication with hashed and salted passwords.
- **Role-based Authorization:** Policies to restrict and manage access.
- **Dynamic Field Validation API:** Checks for existing user data dynamically.
- **JWT Token Management:** Authentication and authorization with custom JWT events and policies.

### 🧑‍⚕️ Patient Management
- **Create and Update Patient Details:** Comprehensive APIs for managing patient data.
- **Search with Sophisticated Filters:** Dynamic filtering options using dictionary and function delegates.
- **Delete Patient Data:** Secure deletion with proper logging and validation.
- **Optimized Querying:** Uses reflections for faster and cleaner queries.

### 🗓️ Appointment Management
- **Doctor Details Management:** CRUD operations for doctor details with robust filtering.
- **Book, Update, and Delete Appointments:** Endpoints designed for flexibility and efficiency.
- **Search Appointments API:** Advanced filtering with dynamic options.

### 💊 Prescription Management
- **Dynamic PDF Generation:** Converts JSON data to PDF for prescriptions using HTML-to-PDF generation logic.
- **Template Design:** APIs to create and manage prescription templates.

### 📊 Clinical Data Management
- **CRUD for Health Data:** APIs for creating, updating, and searching clinical data with validations.
- **Static Data Management:** Efficient retrieval and caching using Redis.
- **Master Data Initialization:** Seed data migration for diseases, symptoms, and prescription templates.

### ⚙️ Infrastructure
- **Docker Integration:** Multi-stage builds for efficient deployment, with simplified container orchestration using Docker Compose.
- **Cloud Database Integration:** Connected to a cloud-hosted PostgreSQL database instance.
- **Health Check API:** Ensures application reliability with automated health checks.
- **Logging and Exception Handling:** Custom logger wrapper and centralized exception handling for enhanced maintainability.

---

## 📜 Commit Highlights

### 🔒 Authentication Management
- **Login API:** Securely handles user logins (`f63a0bf`).
- **Register User API:** Includes hashed and salted passwords (`31ecf15`).
- **Authentication Repo Initialization:** Table creation and schema design (`f9a71fa`).

### 🧑‍⚕️ Patient Management
- **Search Patient Details:** Optimized filtering and query execution (`9af535a`).
- **Dynamic Features in Update API:** Flexible updates for patient details (`9af535a`).
- **Create Patient Details:** Introduced sophisticated request schema design (`c26c5aa`).

### 🗓️ Appointment Management
- **Search Appointments API:** Filtering and pagination integrated (`b96c9a0`).
- **Doctor Details Management:** CRUD operations and mock data population (`ad35154`).

### 💊 Prescription Management
- **Dynamic PDF Generation:** Robust logic for HTML-to-PDF transformation (`8b89e97`).
- **Prescription Template Design:** Initial template design phase (`a7021f2`).

### ⚙️ Infrastructure and Enhancements
- **JWT Implementation:** Integrated JWT authentication and authorization (`aaaca00`).
- **Redis Cache:** Optimized static data retrieval with caching (`7cc833f`).
- **Seed Data Migration:** Initialized clinical data with diseases, symptoms, and templates (`8bd7ebe`).

---

## 🛠️ Technology Stack

- **Backend:** ASP.NET Core
- **Database:** PostgreSQL
- **Caching:** Redis
- **Containerization:** Docker, Docker Compose
- **Authentication:** JWT
- **Frontend:** Not included in the current scope
- **Deployment:** Multi-stage Docker build, cloud-hosted database integration

---

## 🚀 Getting Started

### ✅ Prerequisites
1. .NET 6 SDK or higher
2. Docker and Docker Compose
3. PostgreSQL instance (local or cloud)

### 🛠️ Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/para-commando/healthcare-appointment-and-record-management-system.git
   ```
2. Navigate to the project directory:
   ```bash
   cd healthcare-appointment-and-record-management-system
   ```
3. Open the micro of your choice and make sure appsettings.json is filled with the configuration data and then run the command:
   ```bash
   dotnet run
   ```

### ⚙️ Configuration
- Update `appsettings.json` with your PostgreSQL connection string and Redis configuration.

---

## 🔮 Future Enhancements

- **Frontend Development:** Integration with a modern UI framework like React or Angular.
- **Real-time Features:** Incorporation of SignalR for real-time notifications.
- **Enhanced Role Management:** Support for custom roles and hierarchical access control.

---

## 👥 Contributors

- **Para-Commando:** Full-stack development, architecture, and API design.

---

## 🙏 Acknowledgments

- **"Ganapati Bappa Morya"** - The first commit, marking the auspicious beginning of this journey. 🎉
