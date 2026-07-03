# Driving License Management System (DVLD)

A full desktop application for managing driving license services, built using C#, Windows Forms, SQL Server, ADO.NET, and 3-Tier Architecture.

This project simulates a real Driving and Vehicle License Department (DVLD) system. It manages people, system users, driving license applications, tests, license issuing, license renewal, replacement licenses, detained licenses, and international driving licenses.

---

## Project Overview

The system is designed to organize and automate the main services of a driving license department. It allows employees to manage applicants, create applications, schedule tests, issue licenses, renew licenses, replace lost or damaged licenses, detain and release licenses, and issue international licenses.

The project focuses on applying real-world software development concepts such as Object-Oriented Programming, database design, business rules, reusable user controls, events, delegates, and layered architecture.

---

## Technologies Used

- C#
- Windows Forms
- SQL Server
- ADO.NET
- 3-Tier Architecture
- Object-Oriented Programming (OOP)
- User Controls
- Delegates and Events
- Stored Procedures
- SQL Queries
- DataGridView
- Visual Studio

---

## Architecture

The project follows a 3-Tier Architecture:

### 1. Presentation Layer

This layer contains all Windows Forms screens and User Controls.  
It is responsible for interacting with the user and displaying data.

Examples:

- Login Form
- Main Form
- Manage People
- Manage Users
- Manage Local Driving License Applications
- Schedule Test
- Take Test
- Issue License
- Renew License
- Detain License
- International License

### 2. Business Layer

This layer contains the business logic of the system.  
It handles validations, rules, and communication between the Presentation Layer and the Data Access Layer.

Examples:

- Checking if a person already has an active application
- Checking if a license is active
- Checking if a license is detained
- Checking test results
- Calculating fees
- Managing application status

### 3. Data Access Layer

This layer is responsible for connecting to SQL Server and performing database operations.

Examples:

- Insert
- Update
- Delete
- Search
- Get by ID
- List records
- Execute stored procedures

---

## Main Features

### People Management

- Add new person
- Update person information
- Delete person
- Search by Person ID or National Number
- Display person details
- Handle personal image
- Prevent duplicate National Numbers

### Users Management

- Add new system user
- Update user information
- Delete user
- Activate or deactivate user account
- Change password
- Login system
- Account settings
- Connect each user with a person record

### Login System

- Secure login screen
- Check username and password
- Prevent inactive users from logging in
- Store current logged-in user
- Support account settings and password change

### Application Types Management

- List application types
- Edit application type fees
- Manage services such as:
  - New local driving license application
  - Retake test
  - Renew license
  - Replace lost license
  - Replace damaged license
  - Release detained license
  - Issue international license

### Test Types Management

- List test types
- Edit test type fees
- Manage test types:
  - Vision Test
  - Written Test
  - Street Test

### Local Driving License Applications

- Add new local driving license application
- Update application
- Delete or cancel application
- Filter applications
- Show application details
- Prevent duplicate active applications for the same person and license class
- Track passed tests count

### Test Scheduling

- Schedule Vision Test
- Schedule Written Test
- Schedule Street Test
- Prevent scheduling tests in the wrong order
- Prevent duplicate unlocked appointments
- Lock appointment after taking the test

### Take Test

- Record test result
- Add test notes
- Save pass or fail status
- Lock test appointment after result entry

### Retake Test

- Allow applicants to retake failed tests
- Create retake test application
- Calculate retake test fees
- Link retake application with the original application

### License Issuing

- Issue driving license for the first time
- Create driver record if the person is not already a driver
- Save license issue date and expiration date
- Save license class
- Save paid fees
- Mark application as completed

### Drivers Management

- List all drivers
- Show driver information
- Show local license history
- Show international license history

### Renew Driving License

- Renew expired local driving license
- Create renewal application
- Issue a new license
- Deactivate old license
- Save renewal notes and fees

### Replacement License

- Replace lost license
- Replace damaged license
- Create replacement application
- Deactivate old license
- Issue new license with correct issue reason

### Detain License

- Detain an active license
- Add fine fees
- Save detain date
- Save created by user
- Prevent detaining the same license more than once

### Release Detained License

- Release detained license after paying fine
- Create release application
- Save release date
- Save released by user
- Update detained license status

### International License

- Issue international driving license
- Allow only valid local driving license holders
- Prevent issuing international license for expired or detained licenses
- Deactivate old active international licenses when issuing a new one
- Keep international license history

---

## Database Main Tables

The system uses a SQL Server database that includes tables such as:

- People
- Countries
- Users
- Applications
- ApplicationTypes
- LocalDrivingLicenseApplications
- LicenseClasses
- TestTypes
- TestAppointments
- Tests
- Drivers
- Licenses
- DetainedLicenses
- InternationalLicenses

---

## Business Rules

Some important business rules implemented in the system:

- A person cannot be duplicated using the same National Number.
- A user must be linked to an existing person.
- Inactive users cannot log in.
- A person cannot have more than one active application for the same license class.
- A person must pass Vision Test before Written Test.
- A person must pass Written Test before Street Test.
- A license cannot be issued until all required tests are passed.
- A detained license cannot be renewed or replaced until released.
- A person cannot get an international license unless they have a valid local driving license.
- Old licenses are deactivated when renewed or replaced.
- All important actions are linked with the user who created them.

---

## What I Learned

Through this project, I improved my skills in:

- Building real desktop applications using Windows Forms
- Applying Object-Oriented Programming in a large project
- Designing and working with SQL Server databases
- Using ADO.NET for database connectivity
- Writing clean business logic
- Separating code into Presentation, Business, and Data Access layers
- Creating reusable User Controls
- Working with Delegates and Events
- Handling forms communication
- Managing application workflow and business rules
- Debugging and organizing a large project

---

## Screenshots

Add your screenshots inside a folder named `Screenshots`.

Example:

```text
Screenshots/
│
├── login.png
├── main-form.png
├── manage-people.png
├── manage-users.png
├── local-driving-applications.png
├── license-info.png
└── international-license.png
