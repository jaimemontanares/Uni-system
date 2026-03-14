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
                Console.WriteLine("[1] Create Course");
                Console.WriteLine("[2] Enroll Student to Course");
                Console.WriteLine("[3] Print Courses and Participants");
                Console.WriteLine("[4] Search Course");
                Console.WriteLine("[5] Search Book");
                Console.WriteLine("[6] Loan Book");
                Console.WriteLine("[7] Return Book");
                Console.WriteLine("[8] Register Book");
                Console.WriteLine("[0] Exit");
                Console.Write("\nSelect option: ");

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
                        PrintCourses();
                        break;
                    case "4":
                        SearchCourse();
                        break;
                    case "5":
                        SearchBook();
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
                        Console.WriteLine("Invalid option.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nPress any key to continue...");
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
                Console.WriteLine("Student or course not found.");
                return;
            }

            if (course.EnrollStudent(student))
            {
                Console.WriteLine("Enrollment successful.");
            }
            else
            {
                Console.WriteLine("Enrollment failed. Student may already be enrolled or course is full.");
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
