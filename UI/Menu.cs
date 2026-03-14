C#
using System;
using System.Linq;
using UniversitySystem.Models;
using UniversitySystem.Services;

namespace UniversitySystem.UI
{
    public class Menu
    {
        private readonly UniversityManager _manager;

        public Menu(UniversityManager manager)
        {
            _manager = manager;
        }

        public void Run()
        {
            bool running = true;

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

                string choice = Console.ReadLine();

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
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Ugyldig valg.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nTrykk en tast for å fortsette...");
                    Console.ReadKey();
                }
            }
        }

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

            var course = new Course
            {
                CourseCode = code,
                CourseName = name,
                Credits = credits,
                MaxStudents = maxStudents
            };

            _manager.AddCourse(course);
            Console.WriteLine("Course created successfully.");
        }

        private void EnrollStudent()
        {
            Console.Write("Student Email: ");
            string email = Console.ReadLine();

            Console.Write("Course Code: ");
            string code = Console.ReadLine();

            var student = _manager.FindStudentByEmail(email);
            var course = _manager.FindCourseByCode(code);

            if (student == null || course == null)
            {
                Console.WriteLine("Student eller kurs ble ikke funnet.");
                return;
            }

            if (course.EnrollStudent(student))
            {
                Console.WriteLine("Student successfully enrolled.");
            }
            else
            {
                Console.WriteLine("Enrollment failed. Student may already be enrolled or course is full.");
            }
        }

        private void CourseOverviewMenu()
        {
            Console.WriteLine("\n=== Kursoversikt ===");
            Console.WriteLine("[1] Print kurs og deltagere");
            Console.WriteLine("[2] Meld student av kurs");
            Console.Write("Velg alternativ: ");

            string choice = Console.ReadLine();

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

        private void PrintCourses()
        {
            if (!_manager.Courses.Any())
            {
                Console.WriteLine("No courses found.");
                return;
            }

            foreach (var course in _manager.Courses)
            {
                Console.WriteLine($"\n{course.CourseCode}: {course.CourseName} ({course.EnrolledStudents.Count}/{course.MaxStudents})");

                if (!course.EnrolledStudents.Any())
                {
                    Console.WriteLine(" - No students enrolled");
                    continue;
                }

                foreach (var student in course.EnrolledStudents)
                {
                    Console.WriteLine($" - {student.Name} ({student.Email})");
                }
            }
        }

        private void RemoveStudentFromCourse()
        {
            Console.Write("Student Email: ");
            string email = Console.ReadLine();

            Console.Write("Course Code: ");
            string code = Console.ReadLine();

            if (_manager.RemoveStudentFromCourse(email, code))
            {
                Console.WriteLine("Student removed from course successfully.");
            }
            else
            {
                Console.WriteLine("Could not remove student. Check email, course code, or enrollment status.");
            }
        }

        private void SearchCourse()
        {
            Console.Write("Enter search term: ");
            string term = Console.ReadLine();

            var results = _manager.SearchCourses(term);

            if (!results.Any())
            {
                Console.WriteLine("No matching courses found.");
                return;
            }

            foreach (var course in results)
            {
                Console.WriteLine($"{course.CourseCode} | {course.CourseName} | Credits: {course.Credits}");
            }
        }

        private void LibraryOverviewMenu()
        {
            Console.WriteLine("\n=== Bibliotek ===");
            Console.WriteLine("[1] Søk på bok");
            Console.WriteLine("[2] Vis aktive lån");
            Console.WriteLine("[3] Vis lånehistorikk");
            Console.Write("Velg alternativ: ");

            string choice = Console.ReadLine();

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

        private void SearchBook()
        {
            Console.Write("Enter Book Title: ");
            string title = Console.ReadLine();

            var books = _manager.SearchBooks(title);

            if (!books.Any())
            {
                Console.WriteLine("No matching books found.");
                return;
            }

            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.Id} | {book.Title} by {book.Author} | Available: {book.AvailableCopies}/{book.TotalCopies}");
            }
        }

        private void ShowActiveLoans()
        {
            var activeLoans = _manager.GetActiveLoans().ToList();

            if (!activeLoans.Any())
            {
                Console.WriteLine("No active loans.");
                return;
            }

            Console.WriteLine("\n=== Active Loans ===");
            foreach (var loan in activeLoans)
            {
                Console.WriteLine(
                    $"Book: {loan.Book.Title} | User: {loan.User.Name} ({loan.User.Email}) | Loan Date: {loan.LoanDate:g}");
            }
        }

        private void ShowLoanHistory()
        {
            var history = _manager.GetLoanHistory().ToList();

            if (!history.Any())
            {
                Console.WriteLine("No loan history found.");
                return;
            }

            Console.WriteLine("\n=== Loan History ===");
            foreach (var loan in history)
            {
                string status = loan.ReturnDate == null
                    ? "Active"
                    : $"Returned: {loan.ReturnDate:g}";

                Console.WriteLine(
                    $"Book: {loan.Book.Title} | User: {loan.User.Name} ({loan.User.Email}) | Loaned: {loan.LoanDate:g} | {status}");
            }
        }

        private void ProcessLoan()
        {
            Console.Write("Book ID: ");
            int id = ReadInt();

            Console.Write("User Email: ");
            string email = Console.ReadLine();

            if (_manager.LoanBook(id, email))
            {
                Console.WriteLine("Loan successful.");
            }
            else
            {
                Console.WriteLine("Loan failed. Check book availability and user email.");
            }
        }

        private void ProcessReturn()
        {
            Console.Write("Book ID: ");
            int id = ReadInt();

            Console.Write("User Email: ");
            string email = Console.ReadLine();

            if (_manager.ReturnBook(id, email))
            {
                Console.WriteLine("Book returned successfully.");
            }
            else
            {
                Console.WriteLine("No active loan found.");
            }
        }

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

        private int ReadInt()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out int value))
                {
                    return value;
                }

                Console.Write("Invalid number, try again: ");
            }
        }
    }
}
