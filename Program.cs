C#
using System;
using System.Linq;

namespace UniversitySystem
{
    class Program
    {
        static UniversityManager manager = new UniversityManager();

        static void Main(string[] args)
        {
            SeedData(); // Initialize with some data
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
                    case "1": CreateCourse(); break;
                    case "2": EnrollStudent(); break;
                    case "3": PrintCourses(); break;
                    case "4": SearchCourse(); break;
                    case "5": SearchBook(); break;
                    case "6": ProcessLoan(); break;
                    case "7": ProcessReturn(); break;
                    case "8": RegisterBook(); break;
                    case "0": running = false; break;
                    default: Console.WriteLine("Invalid option."); break;
                }
                if (running) { Console.WriteLine("\nPress any key to continue..."); Console.ReadKey(); }
            }
        }

        // --- Menu Methods ---

        static void CreateCourse()
        {
            Console.Write("Enter Course Code: "); string code = Console.ReadLine();
            Console.Write("Enter Course Name: "); string name = Console.ReadLine();
            Console.Write("Max Students: "); int max = int.Parse(Console.ReadLine());
            manager.AddCourse(new Course { CourseCode = code, CourseName = name, MaxStudents = max });
            Console.WriteLine("Course created successfully!");
        }

        static void EnrollStudent()
        {
            Console.Write("Student Email: "); string email = Console.ReadLine();
            Console.Write("Course Code: "); string code = Console.ReadLine();

            var student = manager.Users.OfType<Student>().FirstOrDefault(s => s.Email == email);
            var course = manager.Courses.FirstOrDefault(c => c.CourseCode == code);

            if (student != null && course != null)
            {
                if (course.EnrollStudent(student)) Console.WriteLine("Enrollment successful!");
                else Console.WriteLine("Course is full!");
            }
            else Console.WriteLine("Student or Course not found.");
        }

        static void PrintCourses()
        {
            foreach (var course in manager.Courses)
            {
                Console.WriteLine($"\n{course.CourseCode}: {course.CourseName} ({course.EnrolledStudents.Count}/{course.MaxStudents})");
                foreach (var s in course.EnrolledStudents) Console.WriteLine($" - {s.Name}");
            }
        }

        static void SearchCourse()
        {
            Console.Write("Enter search term: ");
            string term = Console.ReadLine();
            var results = manager.SearchCourses(term);
            foreach (var c in results) Console.WriteLine($"{c.CourseCode} | {c.CourseName}");
        }

        static void SearchBook()
        {
            Console.Write("Enter Book Title: ");
            string title = Console.ReadLine();
            var books = manager.Books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            foreach (var b in books) Console.WriteLine($"ID: {b.Id} | {b.Title} by {b.Author} (Available: {b.AvailableCopies})");
        }

        static void ProcessLoan()
        {
            Console.Write("Book ID: "); int id = int.Parse(Console.ReadLine());
            Console.Write("User Email: "); string email = Console.ReadLine();
            if (manager.LoanBook(id, email)) Console.WriteLine("Loan successful!");
            else Console.WriteLine("Loan failed (Check availability/User email).");
        }

        static void ProcessReturn()
        {
            Console.Write("Book ID: "); int id = int.Parse(Console.ReadLine());
            Console.Write("User Email: "); string email = Console.ReadLine();
            if (manager.ReturnBook(id, email)) Console.WriteLine("Book returned!");
            else Console.WriteLine("No active loan found.");
        }

        static void RegisterBook()
        {
            Console.Write("Title: "); string t = Console.ReadLine();
            Console.Write("Author: "); string a = Console.ReadLine();
            Console.Write("Copies: "); int c = int.Parse(Console.ReadLine());
            manager.RegisterBook(new Book { Id = manager.Books.Count + 1, Title = t, Author = a, TotalCopies = c, AvailableCopies = c });
            Console.WriteLine("Book registered!");
        }

        static void SeedData()
        {
            manager.Users.Add(new Student("Alice Smith", "alice@uni.com", "S101"));
            manager.Users.Add(new Employee("Dr. Bob", "bob@uni.com", "E001", "Lecturer", "CS"));
            manager.Books.Add(new Book { Id = 1, Title = "C# in Depth", Author = "Jon Skeet", TotalCopies = 2, AvailableCopies = 2 });
            manager.Courses.Add(new Course { CourseCode = "CS101", CourseName = "Intro to Programming", MaxStudents = 30 });
        }
    }
}