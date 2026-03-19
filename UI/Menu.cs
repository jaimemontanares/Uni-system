using System;
using System.Linq;
using UniversitySystem.Models;
using UniversitySystem.Services;

namespace UniversitySystem.UI
{
    /// <summary>
    /// Håndterer all brukerinteraksjon i konsollen.
    /// Klassen viser menyer, leser inn valg fra brukeren
    /// og kaller riktig funksjonalitet i UniversityManager.
    /// </summary>
    public class Menu
    {
        // Referanse til systemets hovedlogikk.
        private readonly UniversityManager _manager;

        /// <summary>
        /// Oppretter en ny meny med tilgang til systemets tjenester.
        /// </summary>
        /// <param name="manager">Instans av UniversityManager som håndterer logikken.</param>
        public Menu(UniversityManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Starter hovedmenyen og holder programmet kjørende
        /// helt til brukeren velger å avslutte.
        /// </summary>
        public void Run()
        {
            // Styrer om hovedløkken skal fortsette å kjøre.
            bool running = true;

            // Viser menyen på nytt helt til brukeren avslutter.
            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== UNIVERSITY MANAGEMENT SYSTEM ===");
                Console.WriteLine("[1] Opprett kurs");
                Console.WriteLine("[2] Meld student til kurs");
                Console.WriteLine("[3] Print kurs og deltagere");
                Console.WriteLine("[4] Søk på kurs");
                Console.WriteLine("[5] Søk på bok");
                Console.WriteLine("[6] Lån bok");
                Console.WriteLine("[7] Returner bok");
                Console.WriteLine("[8] Registrer bok");
                Console.WriteLine("[0] Avslutt");
                Console.Write("\nVelg alternativ: ");

                // Leser inn brukerens menyvalg.
                string choice = Console.ReadLine();

                // Utfører riktig handling basert på valgt menyvalg.
                switch (choice)
                {
                    case "1":
                        CreateCourse();
                        break;
                    case "2":
                        EnrollStudent();
                        break;
                    case "3":
                        CourseOverviewMenu();
                        break;
                    case "4":
                        SearchCourse();
                        break;
                    case "5":
                        LibraryOverviewMenu();
                        break;
                    case "6":
                        ProcessLoan();
                        break;
                    case "7":
                        ProcessReturn();
                        break;
                    case "8":
                        RegisterBook();
                        break;
                    case "0":
                        // Stopper løkken og avslutter programmet.
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Ugyldig valg.");
                        break;
                }

                // Gir brukeren tid til å lese resultatet før skjermen tømmes.
                if (running)
                {
                    Console.WriteLine("\nTrykk en tast for å fortsette...");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Leser inn informasjon om et nytt kurs
        /// og registrerer det i systemet.
        /// </summary>
        private void CreateCourse()
        {
            Console.Write("Enter Course Code: ");
            string code = Console.ReadLine();

            Console.Write("Enter Course Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Credits: ");
            int credits = ReadInt();

            Console.Write("Max Students: ");
            int maxStudents = ReadInt();

            // Oppretter et nytt kursobjekt basert på brukerens input.
            var course = new Course
            {
                CourseCode = code,
                CourseName = name,
                Credits = credits,
                MaxStudents = maxStudents
            };

            // Legger kurset til i systemet.
            _manager.AddCourse(course);
            Console.WriteLine("Course created successfully.");
        }

        /// <summary>
        /// Melder en student opp i et kurs basert på e-post og kurskode.
        /// </summary>
        private void EnrollStudent()
        {
            Console.Write("Student Email: ");
            string email = Console.ReadLine();

            Console.Write("Course Code: ");
            string code = Console.ReadLine();

            // Finner student og kurs i systemet.
            var student = _manager.FindStudentByEmail(email);
            var course = _manager.FindCourseByCode(code);

            // Stopper dersom student eller kurs ikke finnes.
            if (student == null || course == null)
            {
                Console.WriteLine("Student eller kurs ble ikke funnet.");
                return;
            }

            // Prøver å melde studenten opp i kurset.
            if (course.EnrollStudent(student))
            {
                Console.WriteLine("Student successfully enrolled.");
            }
            else
            {
                // Oppmelding kan feile dersom studenten allerede er registrert
                // eller kurset har nådd maksimal kapasitet.
                Console.WriteLine("Enrollment failed. Student may already be enrolled or course is full.");
            }
        }

        /// <summary>
        /// Viser undermeny for kursrelaterte funksjoner.
        /// </summary>
        private void CourseOverviewMenu()
        {
            Console.WriteLine("\n=== Kursoversikt ===");
            Console.WriteLine("[1] Print kurs og deltagere");
            Console.WriteLine("[2] Meld student av kurs");
            Console.Write("Velg alternativ: ");

            string choice = Console.ReadLine();

            // Lar brukeren velge mellom å vise kurs eller fjerne student fra kurs.
            switch (choice)
            {
                case "1":
                    PrintCourses();
                    break;
                case "2":
                    RemoveStudentFromCourse();
                    break;
                default:
                    Console.WriteLine("Ugyldig valg.");
                    break;
            }
        }

        /// <summary>
        /// Skriver ut alle kurs og hvilke studenter som er meldt opp.
        /// </summary>
        private void PrintCourses()
        {
            // Stopper dersom systemet ikke inneholder noen kurs.
            if (!_manager.Courses.Any())
            {
                Console.WriteLine("No courses found.");
                return;
            }

            // Går gjennom alle kurs og skriver ut kursinformasjon.
            foreach (var course in _manager.Courses)
            {
                Console.WriteLine($"\n{course.CourseCode}: {course.CourseName} ({course.EnrolledStudents.Count}/{course.MaxStudents})");

                // Skriver egen melding dersom kurset ikke har noen studenter.
                if (!course.EnrolledStudents.Any())
                {
                    Console.WriteLine(" - No students enrolled");
                    continue;
                }

                // Skriver ut alle studenter som er meldt opp i kurset.
                foreach (var student in course.EnrolledStudents)
                {
                    Console.WriteLine($" - {student.Name} ({student.Email})");
                }
            }
        }

        /// <summary>
        /// Fjerner en student fra et kurs basert på e-post og kurskode.
        /// </summary>
        private void RemoveStudentFromCourse()
        {
            Console.Write("Student Email: ");
            string email = Console.ReadLine();

            Console.Write("Course Code: ");
            string code = Console.ReadLine();

            // Forsøker å fjerne studenten fra kurset via service-laget.
            if (_manager.RemoveStudentFromCourse(email, code))
            {
                Console.WriteLine("Student removed from course successfully.");
            }
            else
            {
                Console.WriteLine("Could not remove student. Check email, course code, or enrollment status.");
            }
        }

        /// <summary>
        /// Søker etter kurs ved hjelp av kurskode eller kursnavn.
        /// </summary>
        private void SearchCourse()
        {
            Console.Write("Enter search term: ");
            string term = Console.ReadLine();

            // Henter søkeresultat fra systemet.
            var results = _manager.SearchCourses(term);

            // Stopper dersom ingen kurs matcher søket.
            if (!results.Any())
            {
                Console.WriteLine("No matching courses found.");
                return;
            }

            // Skriver ut alle kurs som matcher søket.
            foreach (var course in results)
            {
                Console.WriteLine($"{course.CourseCode} | {course.CourseName} | Credits: {course.Credits}");
            }
        }

        /// <summary>
        /// Viser undermeny for bibliotekfunksjoner.
        /// </summary>
        private void LibraryOverviewMenu()
        {
            Console.WriteLine("\n=== Bibliotek ===");
            Console.WriteLine("[1] Søk på bok");
            Console.WriteLine("[2] Vis aktive lån");
            Console.WriteLine("[3] Vis lånehistorikk");
            Console.Write("Velg alternativ: ");

            string choice = Console.ReadLine();

            // Lar brukeren velge hvilken bibliotekfunksjon som skal kjøres.
            switch (choice)
            {
                case "1":
                    SearchBook();
                    break;
                case "2":
                    ShowActiveLoans();
                    break;
                case "3":
                    ShowLoanHistory();
                    break;
                default:
                    Console.WriteLine("Ugyldig valg.");
                    break;
            }
        }

        /// <summary>
        /// Søker etter bøker basert på tittel.
        /// </summary>
        private void SearchBook()
        {
            Console.Write("Enter Book Title: ");
            string title = Console.ReadLine();

            // Henter alle bøker som matcher tittelen.
            var books = _manager.SearchBooks(title);

            // Stopper dersom ingen bøker ble funnet.
            if (!books.Any())
            {
                Console.WriteLine("No matching books found.");
                return;
            }

            // Skriver ut informasjon om hver bok som ble funnet.
            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.Id} | {book.Title} by {book.Author} | Available: {book.AvailableCopies}/{book.TotalCopies}");
            }
        }

        /// <summary>
        /// Viser alle utlån som fortsatt er aktive.
        /// </summary>
        private void ShowActiveLoans()
        {
            // Gjør resultatet om til liste for å unngå flere gjennomganger av samme spørring.
            var activeLoans = _manager.GetActiveLoans().ToList();

            // Stopper dersom det ikke finnes aktive lån.
            if (!activeLoans.Any())
            {
                Console.WriteLine("No active loans.");
                return;
            }

            Console.WriteLine("\n=== Active Loans ===");

            // Skriver ut informasjon om alle aktive utlån.
            foreach (var loan in activeLoans)
            {
                Console.WriteLine(
                    $"Book: {loan.Book.Title} | User: {loan.User.Name} ({loan.User.Email}) | Loan Date: {loan.LoanDate:g}");
            }
        }

        /// <summary>
        /// Viser hele lånehistorikken, både aktive og avsluttede utlån.
        /// </summary>
        private void ShowLoanHistory()
        {
            var history = _manager.GetLoanHistory().ToList();

            // Stopper dersom det ikke finnes noen historikk.
            if (!history.Any())
            {
                Console.WriteLine("No loan history found.");
                return;
            }

            Console.WriteLine("\n=== Loan History ===");

            foreach (var loan in history)
            {
                // Viser forskjell på aktive lån og bøker som allerede er returnert.
                string status = loan.ReturnDate == null
                    ? "Active"
                    : $"Returned: {loan.ReturnDate:g}";

                Console.WriteLine(
                    $"Book: {loan.Book.Title} | User: {loan.User.Name} ({loan.User.Email}) | Loaned: {loan.LoanDate:g} | {status}");
            }
        }

        /// <summary>
        /// Registrerer et utlån dersom bok og bruker finnes
        /// og boka er tilgjengelig.
        /// </summary>
        private void ProcessLoan()
        {
            Console.Write("Book ID: ");
            int id = ReadInt();

            Console.Write("User Email: ");
            string email = Console.ReadLine();

            // Forsøker å opprette nytt utlån.
            if (_manager.LoanBook(id, email))
            {
                Console.WriteLine("Loan successful.");
            }
            else
            {
                Console.WriteLine("Loan failed. Check book availability and user email.");
            }
        }

        /// <summary>
        /// Registrerer retur av en bok dersom det finnes et aktivt utlån.
        /// </summary>
        private void ProcessReturn()
        {
            Console.Write("Book ID: ");
            int id = ReadInt();

            Console.Write("User Email: ");
            string email = Console.ReadLine();

            // Forsøker å finne og avslutte et aktivt lån.
            if (_manager.ReturnBook(id, email))
            {
                Console.WriteLine("Book returned successfully.");
            }
            else
            {
                Console.WriteLine("No active loan found.");
            }
        }

        /// <summary>
        /// Leser inn informasjon om en bok og registrerer den i biblioteket.
        /// </summary>
        private void RegisterBook()
        {
            Console.Write("Title: ");
            string title = Console.ReadLine();

            Console.Write("Author: ");
            string author = Console.ReadLine();

            Console.Write("Year: ");
            int year = ReadInt();

            Console.Write("Copies: ");
            int copies = ReadInt();

            // Oppretter nytt bokobjekt med automatisk generert ID.
            // Antall tilgjengelige eksemplarer settes lik totalt antall ved registrering.
            var book = new Book
            {
                Id = _manager.GetNextBookId(),
                Title = title,
                Author = author,
                Year = year,
                TotalCopies = copies,
                AvailableCopies = copies
            };

            _manager.RegisterBook(book);
            Console.WriteLine("Book registered successfully.");
        }

        /// <summary>
        /// Leser inn et heltall fra brukeren.
        /// Fortsetter å spørre til et gyldig tall er skrevet inn.
        /// </summary>
        /// <returns>Et gyldig heltall.</returns>
        private int ReadInt()
        {
            while (true)
            {
                string input = Console.ReadLine();

                // Sikrer at bare gyldige heltall blir akseptert.
                if (int.TryParse(input, out int value))
                {
                    return value;
                }

                Console.Write("Invalid number, try again: ");
            }
        }
    }
}
