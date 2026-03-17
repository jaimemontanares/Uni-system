# Uni-system

C# console application modeling a university administration system for course and library management.

Developed for course assignment **IS-110** (Winter/Spring 2026).

---

## Features

The application currently supports:

### User roles

* Student
* Exchange Student
* Employee

### Course management

* Create courses
* Enroll students in courses
* Remove students from courses
* List courses and participants
* Search courses by code or name

### Library management

* Register books
* Search books
* Loan books
* Return books
* View active loans
* View loan history

### Console menu

```text
[1] Opprett kurs
[2] Meld student til kurs
[3] Print kurs og deltagere
[4] Søk på kurs
[5] Søk på bok
[6] Lån bok
[7] Returner bok
[8] Registrer bok
[0] Avslutt
```

---

## Project Structure

```text
Uni-system/
│
├── Uni-system.csproj
├── Program.cs
├── License
│
├── Models/
│   ├── User.cs
│   ├── Student.cs
│   ├── ExchangeStudent.cs
│   ├── Employee.cs
│   ├── Course.cs
│   ├── Book.cs
│   └── Loan.cs
│
├── Services/
│   └── UniversityManager.cs
│
├── UI/
│   └── Menu.cs
│
└── Data/
    └── SeedData.cs
```

---

## Installation

1. Clone the repository:

```bash
git clone https://github.com/jaimemontanares/Uni-system.git
```

2. Open the project in Visual Studio or Visual Studio Code.

3. Build the project:

```bash
dotnet build
```

4. Run the application:

```bash
dotnet run
```

---

## Usage

When the program starts, a console menu appears.

You can:

* create new courses
* enroll students
* search courses
* register books
* loan and return books
* view course participants
* inspect active and historical loans

The project starts with preloaded demo data through `SeedData`.

---

## Requirements

* .NET SDK 8.0 or later
* Visual Studio 2022 / VS Code recommended

---

## License

Educational use permitted. Commercial use requires permission from the author.

---

## Author

**Jaime Montanares** https://github.com/jaimemontanares

---

## Acknowledgment

README structure inspired by:

https://www.makeareadme.com/

---

## Project Status

Under development.
