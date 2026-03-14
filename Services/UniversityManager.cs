C#
using System;
using System.Collections.Generic;
using System.Linq;
using UniversitySystem.Models;

namespace UniversitySystem.Services
{
    public class UniversityManager
    {
        public List<Course> Courses { get; set; } = new List<Course>();
        public List<User> Users { get; set; } = new List<User>();
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Loan> Loans { get; set; } = new List<Loan>();

        // Course Management
        public void AddCourse(Course course)
        {
            if (course != null)
            {
                Courses.Add(course);
            }
        }

        public List<Course> SearchCourses(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new List<Course>();
            }

            return Courses
                .Where(c =>
                    c.CourseCode.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    c.CourseName.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public Student FindStudentByEmail(string email)
        {
            return Users.OfType<Student>()
                .FirstOrDefault(s => s.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public Course FindCourseByCode(string code)
        {
            return Courses.FirstOrDefault(c =>
                c.CourseCode.Equals(code, StringComparison.OrdinalIgnoreCase));
        }

        // Library Management
        public void RegisterBook(Book book)
        {
            if (book != null)
            {
                Books.Add(book);
            }
        }

        public IEnumerable<Book> SearchBooks(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return Enumerable.Empty<Book>();
            }

            return Books.Where(b =>
                b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
        }

        public bool LoanBook(int bookId, string userEmail)
        {
            var book = Books.FirstOrDefault(b => b.Id == bookId);
            var user = Users.FirstOrDefault(u =>
                u.Email.Equals(userEmail, StringComparison.OrdinalIgnoreCase));

            if (book == null || user == null || book.AvailableCopies <= 0)
            {
                return false;
            }

            Loans.Add(new Loan
            {
                Book = book,
                User = user,
                LoanDate = DateTime.Now
            });

            book.AvailableCopies--;
            return true;
        }

        public bool ReturnBook(int bookId, string userEmail)
        {
            var loan = Loans.FirstOrDefault(l =>
                l.Book.Id == bookId &&
                l.User.Email.Equals(userEmail, StringComparison.OrdinalIgnoreCase) &&
                l.ReturnDate == null);

            if (loan == null)
            {
                return false;
            }

            loan.ReturnDate = DateTime.Now;
            loan.Book.AvailableCopies++;
            return true;
        }

        public int GetNextBookId()
        {
            if (!Books.Any())
            {
                return 1;
            }

            return Books.Max(b => b.Id) + 1;
        }
    }
}
