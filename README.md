# Medical Data Management System

## Objective:
Design and implement a database system to manage patient, doctor, and appointment records. Provide functionality to perform CRUD (Create, Read, Update, Delete) operations, generate reports, and visualize trends. This can be expanded with a front-end interface using C# or ASP.NET.

## Project Description:
### Backend Database Development (MS SQL)
#### Schema Design:
Create a database schema with the following tables:

Patients: Stores patient details (e.g., ID, Name, Age, Gender, Contact Info).
Doctors: Stores doctor details (e.g., ID, Name, Specialty, Contact Info).
Appointments: Links patients with doctors and records appointment dates and diagnoses.
Medications: Links prescriptions to patients and doctors.
Data Ingestion:
Populate these tables with synthetic data from publicly available datasets.

Queries:
Implement queries for:

Retrieving all appointments for a specific doctor within a date range.
Listing all patients prescribed a specific medication.
Summarizing the number of appointments per doctor by specialty.
