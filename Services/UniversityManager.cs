C#
using System;
using System.Collections.Generic;
using System.Linq;

namespace UniversitySystem
{
    public class UniversityManager
    {
        public List<Course> Courses { get; set; } = new List<Course>();
        public List<User> Users { get; set; } = new List<User>();
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Loan> Loans { get; set; } = new List<Loan>();

        // Course Management
        public void AddCourse(Course course) => Courses.Add(course);

        public List<Course> SearchCourses(string query)
        {
            return Courses.Where(c => c.CourseCode.Contains(query, StringComparison.OrdinalIgnoreCase) || 
                                     c.CourseName.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Library Management
        public void RegisterBook(Book book) => Books.Add(book);

        public bool LoanBook(int bookId, string userEmail)
        {
            var book = Books.FirstOrDefault(b => b.Id == bookId);
            var user = Users.FirstOrDefault(u => u.Email == userEmail);

            if (book != null && user != null && book.AvailableCopies > 0)
            {
                book.AvailableCopies--;
                Loans.Add(new Loan { Book = book, User = user, LoanDate = DateTime.Now });
                return true;
            }
            return false;
        }

        public bool ReturnBook(int bookId, string userEmail)
        {
            var loan = Loans.FirstOrDefault(l => l.Book.Id == bookId && l.User.Email == userEmail && l.ReturnDate == null);
            if (loan != null)
            {
                loan.ReturnDate = DateTime.Now;
                loan.Book.AvailableCopies++;
                return true;
            }
            return false;
        }
    }
}