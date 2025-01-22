# Medical Data Management System

## Objective:

Design and implement a database system to manage patient, doctor, and appointment records. Provide functionality to perform CRUD (Create, Read, Update, Delete) operations, generate reports, and visualize trends. This can be expanded with a front-end interface using C# or ASP.NET.

## Project Description:

### Step 1: Setting Up Your Environment

1. Install MS SQL Server (or Teradata):  

* Download and install MS SQL Server and SQL Server Management Studio (SSMS).

2. Install Development Tools for Frontend: 

* Download and install Visual Studio (2022 or later).  
* During installation, select the ASP.NET and Web Development workload.

3. Prepare Datasets:  

* Download the datasets from the links provided:  
* Patients, Doctors, Appointments, and Medications.  
* Save them to a local folder for importing into your database.  

### Step 2: Database Setup

1. Create a New Database

* Open SSMS (or Teradata Studio).
* Connect to your database server and create a new database (e.g., MedicalDB):
```
CREATE DATABASE MedicalDB;
USE MedicalDB;
```

2. Create Tables

* Write SQL scripts to create tables for Patients, Doctors, Appointments, and Medications.
* Example for the Patients table:
```
CREATE TABLE Patients (
    PatientID NVARCHAR(10) PRIMARY KEY,
    Name NVARCHAR(50),
    Age INT,
    Gender NVARCHAR(10),
    ContactInfo NVARCHAR(15)
);
```
Repeat for the other tables (Doctors, Appointments, Medications) based on the structure in the datasets.

3. Import Data into Tables
   
* Use the Import Data Wizard in SSMS to load data from CSV files into the respective tables:   
* Right-click your database → Tasks → Import Flat File → Follow the wizard. 

### Step 3: Backend Development

1. Write Basic Queries

Test the following queries in SSMS:  
* Retrieve all appointments for a specific doctor:  
```
SELECT * 
FROM Appointments
WHERE DoctorID = 'D01';
```
* List all patients prescribed a specific medication:
```
SELECT DISTINCT Patients.Name, Medications.MedicationName
FROM Patients
JOIN Medications ON Patients.PatientID = Medications.PatientID
WHERE Medications.MedicationName = 'Aspirin';
```
### Step 4: Frontend Development (ASP.NET with C#)

1. Create an ASP.NET Project
* Open Visual Studio and create a new ASP.NET Core Web App (Model-View-Controller) project.
* Name your project (e.g., MedicalDataApp).

2. Set Up Database Connection  

Add a connection string in the appsettings.json file to connect to your database:
```
"ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=MedicalDB;Trusted_Connection=True;"
}
```
3. Install Entity Framework Core

Entity Framework Core (EF Core) is the ORM (Object-Relational Mapper) used to interact with the database. 

Open NuGet Package Manager:
* Right-click the project in Solution Explorer and choose Manage NuGet Packages.
   
Go to the Browse tab and search for:  
```
Microsoft.EntityFrameworkCore.SqlServer
```
Install the Package:
* Select the latest version and click Install.
* This package allows your application to work with SQL Server.

### Step 5: Add the Database Context
The DbContext class represents the connection to the database and provides methods to query and save data.

Create a New Class:

In the Models folder (or a new folder you create), add a new class called MedicalDbContext.

Define the DbContext:

Write the following code:
```
using Microsoft.EntityFrameworkCore;

public class MedicalDbContext : DbContext
{
    public MedicalDbContext(DbContextOptions<MedicalDbContext> options) : base(options) { }

    // Define tables as DbSet<T>
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Medication> Medications { get; set; }
}
```

Register the DbContext in Program

Open the Program.cs file.

Register the DbContext in the ConfigureServices method. Add this line:
```
builder.Services.AddDbContext<MedicalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```
This tells the app to use SQL Server and links the MedicalDbContext to the connection string defined in appsettings.json.

Ensure you import the necessary namespaces:
```
using Microsoft.EntityFrameworkCore;
```
### Step 6: Create Models
Create C# models for each table (Patient, Doctor, Appointment, Medication) in the Models folder. 

Example:

1. Patient Model
```
public class Patient
{
    public string PatientID { get; set; }  // Primary Key
    public string Name { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string ContactInfo { get; set; }
}
```
2. Doctor Model
```
public class Doctor
{
    public string DoctorID { get; set; }  // Primary Key
    public string Name { get; set; }
    public string Specialty { get; set; }
    public string ContactInfo { get; set; }
}
```
3. Appointment Model
```
public class Appointment
{
    public string AppointmentID { get; set; }  // Primary Key
    public string PatientID { get; set; }      // Foreign Key
    public string DoctorID { get; set; }       // Foreign Key
    public DateTime AppointmentDate { get; set; }
    public string Purpose { get; set; }

    // Navigation Properties
    public Patient Patient { get; set; }
    public Doctor Doctor { get; set; }
}
```
4. Medication Model
```
public class Medication
{
    public string MedicationID { get; set; }   // Primary Key
    public string PatientID { get; set; }      // Foreign Key
    public string MedicationName { get; set; }
    public string Dosage { get; set; }
    public string Frequency { get; set; }

    // Navigation Property
    public Patient Patient { get; set; }
}
```
Key Notes:

Primary Keys:

* The PatientID, DoctorID, AppointmentID, and MedicationID are primary keys that uniquely identify each record.

Foreign Keys:
* The PatientID and DoctorID in the Appointment table link to the respective Patient and Doctor tables.
* The PatientID in the Medication table links to the Patient table.

Navigation Properties:
* Navigation properties (Patient and Doctor) allow you to retrieve related records using Entity Framework. For example, you can retrieve the doctor associated with a specific appointment.

Data Types:
* string is used for IDs and text fields.
* DateTime is used for the AppointmentDate.

### Step 7: Apply Migrations

Open the Package Manager Console.

Run:
```
Add-Migration InitialCreate
Update-Database
```
This will create the database schema in SQL Server based on your models if not present.

Modify and Apply New Migrations (If Needed)

If you modify your models (e.g., adding a new field or changing a table structure), follow these steps to create a new migration and update the database:

* Modify your model(s) (e.g., add new properties).
* In Package Manager Console, create a new migration:
```
Add-Migration AddNewFieldToPatients
Update-Database
```

This will update the database schema based on the new migration.

### Step 8: Generate Controllers and Views

Add Custom Controllers and Views:

* To build features like managing patients or doctors, you need to create controllers and views.
* Use the Scaffold feature to generate controllers and views for CRUD operations:
  - Right-click the Controllers folder → Add → Controller → MVC with Views using Entity Framework → Follow the wizard.
  - Select the model (e.g., Patient) and context class (e.g., MedicalDbContext).
* Visual Studio automatically generates views for CRUD operations (e.g., Create, Edit, Details).

### Step 9: Customize front-end

To customize the front-end and start building your own app, follow these steps:

1. Locate the Default View

* The default content is being rendered from the Views folder in your ASP.NET Core project.
* Open the Project in Visual Studio or VS Code.

Navigate to Views > Home, You'll see two key files:

* Index.cshtml (for the homepage content).
* Privacy.cshtml (for the privacy policy page).

2. Edit the Homepage Content

Open Index.cshtml:

It might look something like this:

```
@page
@model MedicalDataApps.Pages.IndexModel
@{
    ViewData["Title"] = "Home Page";
}
```
```
<div class="text-center">
    <h1 class="display-4">Welcome</h1>
    <p>Learn about building Web apps with ASP.NET Core.</p>
</div>
```

Modify the content:

Replace the placeholder text with your desired homepage content. For example:

```
<div class="text-center">
    <h1 class="display-4">Welcome to Medical Data Management</h1>
    <p>Manage patients, doctors, appointments, and medications with ease.</p>
    <a class="btn btn-primary" href="/Patients">Get Started</a>
</div>
```

3. Customize Navigation Menu

The navigation menu (Home, Privacy, etc.) is defined in the _Layout.cshtml file.

* Go to: Views > Shared > _Layout.cshtml
* Update the menu links - Look for the <nav> section, which may look like this:
```
<ul class="navbar-nav">
    <li class="nav-item">
        <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Index">Home</a>
    </li>
    <li class="nav-item">
        <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
    </li>
</ul>
```
* Add links for your custom pages. For example:
```
<ul class="navbar-nav">
    <li class="nav-item">
        <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Index">Home</a>
    </li>
    <li class="nav-item">
        <a class="nav-link text-dark" asp-area="" asp-controller="Patients" asp-action="Index">Patients</a>
    </li>
    <li class="nav-item">
        <a class="nav-link text-dark" asp-area="" asp-controller="Doctors" asp-action="Index">Doctors</a>
    </li>
    <li class="nav-item">
        <a class="nav-link text-dark" asp-area="" asp-controller="Appointments" asp-action="Index">Appointments</a>
    </li>
</ul>
```

4.Test the Pages:

Run the app and navigate to the new routes (e.g., /Patients).


### Step 10: Run the Application
Save all changes.
Run the app (Ctrl + F5 or dotnet run in the terminal).
Navigate to the updated homepage and new pages you’ve created.
