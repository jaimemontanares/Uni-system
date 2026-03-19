# Uni-system

A simple university administration system built in C# as a learning project.
The application simulates core university operations such as student enrollment, course management, and library book loans through a console-based interface.

Developed for course assignment **IS-110** (Winter/Spring 2026).

---

## Project Purpose

This project was created to practice object-oriented programming in C# and understand how different parts of a software system can be organized into separate responsibilities.

The goal is to simulate how a university system may manage:

* Students
* Courses
* Books
* Book loans
* Enrollment processes

This project focuses on:

* Classes and objects
* Lists and collections
* Methods and business logic
* Console interaction
* Basic project architecture

---

## Project Structure

```plaintext
Uni-system/
│
├── Uni-system.csproj
├── Program.cs                     # Application entry point
├── Custom Educational License     # License
│
├── Models/                        # Data classes such as Student, Course, Book, Loan
│   ├── User.cs
│   ├── Student.cs
│   ├── ExchangeStudent.cs
│   ├── Employee.cs
│   ├── Course.cs
│   ├── Book.cs
│   └── Loan.cs
│
├── Services/                      # Core business logic
│   └── UniversityManager.cs
│
├── UI/                            # Console menu and user interaction
│   └── Menu.cs
│
└── Data/                          # Seed data for demo/testing
    └── SeedData.cs
```

### Folder Responsibilities

| Folder   | Responsibility                                  |
| -------- | ----------------------------------------------- |
| Models   | Defines the core entities used in the system    |
| Services | Handles business rules and operations           |
| UI       | Displays menus and receives user input          |
| Data     | Creates sample data when the application starts |

---

## Application Flow

The program starts in `Program.cs`.

### Startup sequence:

```plaintext
Program.cs
   ↓
SeedData.Initialize()
   ↓
Menu.Run()
```

### Explanation

1. `Program.cs` starts the application
2. `SeedData` creates demo students, books, and courses
3. `Menu` launches the console menu for user interaction

This makes it possible to test features immediately without entering all data manually.

---

## Features
The application currently supports:
### User roles
* Student
* Exchange Student
* Employee

### Student Management
* Add students
* Remove students
* View all students

### Course Management
* Create courses
* Enroll students
* Remove students from courses
* View enrolled students

### Library Management
* Register books
* Loan books
* Return books
* View available books

---

## Business Rules

The system applies several basic validation rules:

### Enrollment rules

* A student cannot be enrolled twice in the same course
* A course cannot exceed maximum capacity

### Library rules

* A book cannot be borrowed if no copies are available
* Only active loans can be returned

These rules are implemented inside the service and model classes.

---
## Usage

When the program starts, a console menu appears.

### Console menu

```text
[1] Opprett kurs                 # create new courses
[2] Meld student til kurs        # enroll students
[3] Print kurs og deltagere      # view course participants
[4] Søk på kurs                  # search courses<
[5] Søk på bok                   # search books
[6] Lån bok                      # loan books
[7] Returner bok                 # return books
[8] Registrer bok                # register books
[0] Avslutt                      # Close the application
```

The project starts with preloaded demo data through `SeedData`.

### Example Console Usage

#### Enroll a student

```plaintext
1. Course Management
2. Enroll Student
Enter student email:
student@example.com
Course selected successfully
Student enrolled
```

#### Loan a book

```plaintext
1. Library Management
2. Loan Book
Enter student email:
student@example.com
Enter book title:
C# Fundamentals
Book loan created
```

---

## How to Run

### Requirements

* .NET SDK installed
* Visual Studio or VS Code

### Run the project

```bash
dotnet run
```

---

## Seed Data Included

The project starts with sample data for easier testing.

Example demo data includes:

* Students
* Courses
* Books

This allows immediate testing of all menu options.

---

## Technologies Used

* C#
* .NET Console Application
* Object-Oriented Programming

---

## Learning Goals

This project helped practice:

* Encapsulation
* Separation of concerns
* List handling
* Method design
* Basic validation logic

---

## Current Limitations

This project currently uses in-memory data only.

That means:

* No database
* No file persistence
* Data resets when application closes

Future improvements may include:

* Database integration
* Better input validation
* Search functionality
* Unit testing

---

## Future Improvements

Possible next steps: Pending...

---

## Documentation Notes

This project is intended as a learning project, so the code prioritizes readability over advanced architecture.

---

## Author

**Jaime Montanares** https://github.com/jaimemontanares

---

## Acknowledgment

Parts of the documentation were improved through iterative feedback and AI-assisted writing support, with all final project structure, implementation, and adaptation carried out by the author.

---

## Project Status

Under development.
