using System;
using UniversitySystem.Models;
using UniversitySystem.Services;

namespace UniversitySystem.Data
{
    /// <summary>
    /// Oppretter eksempeldata slik at systemet kan testes ved oppstart.
    /// Denne klassen fyller en eksisterende UniversityManager med demo-data.
    /// </summary>
    public static class SeedData
    {
        /// <summary>
        /// Legger inn eksempelbrukere, kurs, bøker og noen relasjoner mellom dem.
        /// Kaster exception dersom noe av seedingen feiler, slik at feil oppdages tidlig.
        /// </summary>
        /// <param name="manager">Eksisterende UniversityManager som skal fylles med data.</param>
        public static void Initialize(UniversityManager manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager), "UniversityManager kan ikke være null.");
            }

            SeedUsers(manager);
            SeedCourses(manager);
            SeedBooks(manager);
            SeedRelationships(manager);
        }

        /// <summary>
        /// Oppretter eksempelbrukere i systemet.
        /// </summary>
        private static void SeedUsers(UniversityManager manager)
        {
            // Merk: Passordene her er kun demo-passord for testing i utviklingsfasen.
            var student = new Student(
                id: "U1",
                studentId: "S1001",
                name: "Ola Nordmann",
                email: "ola@student.no",
                username: "student1",
                password: "1234");

            var exchangeStudent = new ExchangeStudent(
                id: "U2",
                studentId: "S1002",
                name: "Anna Svensson",
                email: "anna@exchange.no",
                username: "exchange1",
                password: "1234",
                homeUniversity: "Stockholm University",
                country: "Sweden",
                periodFrom: new DateTime(2026, 1, 10),
                periodTo: new DateTime(2026, 6, 10));

            var lecturer = new Lecturer(
                id: "U3",
                employeeId: "E2001",
                name: "Per Hansen",
                email: "per@uia.no",
                username: "lecturer1",
                password: "1234",
                department: "IT");

            var librarian = new Librarian(
                id: "U4",
                employeeId: "E2002",
                name: "Kari Berg",
                email: "kari@uia.no",
                username: "librarian1",
                password: "1234",
                department: "Bibliotek");

            AddUserOrThrow(manager, student, "student");
            AddUserOrThrow(manager, exchangeStudent, "utvekslingsstudent");
            AddUserOrThrow(manager, lecturer, "faglærer");
            AddUserOrThrow(manager, librarian, "bibliotekar");
        }

        /// <summary>
        /// Oppretter eksempelkurs i systemet.
        /// </summary>
        private static void SeedCourses(UniversityManager manager)
        {
            CreateCourseOrThrow(
                manager,
                code: "IS110",
                name: "Objektorientert programmering",
                credits: 10,
                maxCapacity: 30,
                lecturerId: "U3");

            CreateCourseOrThrow(
                manager,
                code: "IS200",
                name: "Databasesystemer",
                credits: 10,
                maxCapacity: 25,
                lecturerId: "U3");
        }

        /// <summary>
        /// Registrerer eksempelbøker i biblioteket.
        /// </summary>
        private static void SeedBooks(UniversityManager manager)
        {
            RegisterBookOrThrow(
                manager,
                id: "B1",
                title: "Clean Code",
                author: "Robert C. Martin",
                totalCopies: 3,
                librarianId: "U4");

            RegisterBookOrThrow(
                manager,
                id: "B2",
                title: "Design Patterns",
                author: "Erich Gamma",
                totalCopies: 2,
                librarianId: "U4");
        }

        /// <summary>
        /// Oppretter noen koblinger mellom brukere og kurs.
        /// </summary>
        private static void SeedRelationships(UniversityManager manager)
        {
            EnrollStudentOrThrow(manager, studentId: "U1", courseCode: "IS110");
            EnrollStudentOrThrow(manager, studentId: "U2", courseCode: "IS200");
        }

        /// <summary>
        /// Legger til en bruker og stopper oppsettet dersom operasjonen feiler.
        /// </summary>
        private static void AddUserOrThrow(UniversityManager manager, User user, string userDescription)
        {
            bool success = manager.UserService.AddUser(user);

            if (!success)
            {
                throw new InvalidOperationException(
                    $"Kunne ikke registrere {userDescription} med id '{user.Id}'.");
            }
        }

        /// <summary>
        /// Oppretter et kurs og stopper oppsettet dersom operasjonen feiler.
        /// </summary>
        private static void CreateCourseOrThrow(
            UniversityManager manager,
            string code,
            string name,
            int credits,
            int maxCapacity,
            string lecturerId)
        {
            bool success = manager.CourseService.CreateCourse(
                code: code,
                name: name,
                credits: credits,
                maxCapacity: maxCapacity,
                lecturerId: lecturerId,
                out string message);

            if (!success)
            {
                throw new InvalidOperationException(
                    $"Kunne ikke opprette kurs '{code} - {name}'. Feil: {message}");
            }
        }

        /// <summary>
        /// Registrerer en bok og stopper oppsettet dersom operasjonen feiler.
        /// </summary>
        private static void RegisterBookOrThrow(
            UniversityManager manager,
            string id,
            string title,
            string author,
            int totalCopies,
            string librarianId)
        {
            bool success = manager.LibraryService.RegisterBook(
                id: id,
                title: title,
                author: author,
                totalCopies: totalCopies,
                librarianId: librarianId,
                out string message);

            if (!success)
            {
                throw new InvalidOperationException(
                    $"Kunne ikke registrere bok '{title}' med id '{id}'. Feil: {message}");
            }
        }

        /// <summary>
        /// Melder opp en student til et kurs og stopper oppsettet dersom operasjonen feiler.
        /// </summary>
        private static void EnrollStudentOrThrow(
            UniversityManager manager,
            string studentId,
            string courseCode)
        {
            bool success = manager.CourseService.EnrollStudent(
                studentId,
                courseCode,
                out string message);

            if (!success)
            {
                throw new InvalidOperationException(
                    $"Kunne ikke melde student '{studentId}' opp til kurs '{courseCode}'. Feil: {message}");
            }
        }
    }
}
