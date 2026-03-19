using System;
using System.Collections.Generic;
using System.Linq;
using UniversitySystem.Models;

namespace UniversitySystem.Services
{
    /// <summary>
    /// Håndterer hovedlogikken i systemet:
    /// kursadministrasjon, brukere, bibliotek og utlån.
    /// </summary>
    public class UniversityManager
    {
        // Liste over alle kurs i systemet.
        public List<Course> Courses { get; set; } = new List<Course>();
        // Liste over alle brukere, inkludert studenter og ansatte.
        public List<User> Users { get; set; } = new List<User>();
        // Liste over registrerte bøker.
        public List<Book> Books { get; set; } = new List<Book>();
        // Liste over alle utlån, både aktive og avsluttede.
        public List<Loan> Loans { get; set; } = new List<Loan>();

        // -------------------------
        // Kursadministrasjon
        // -------------------------

        /// <summary>
        /// Legger til et nytt kurs dersom objektet er gyldig.
        /// </summary>
        public void AddCourse(Course course)
        {
            if (course != null)
            {
                Courses.Add(course);
            }
        }
        
        /// <summary>
        /// Søker etter kurs basert på kurskode eller kursnavn.
        /// </summary>
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

        /// <summary>
        /// Finner en student ved hjelp av e-postadresse.
        /// </summary>
        public Student FindStudentByEmail(string email)
        {
            return Users.OfType<Student>()
                .FirstOrDefault(s => s.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }
        
        /// <summary>
        /// Finner et kurs basert på kurskode.
        /// </summary>
        public Course FindCourseByCode(string code)
        {
            return Courses.FirstOrDefault(c =>
                c.CourseCode.Equals(code, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Fjerner en student fra et kurs dersom studenten er registrert.
        /// </summary>
        public bool RemoveStudentFromCourse(string studentEmail, string courseCode)
        {
            var student = FindStudentByEmail(studentEmail);
            var course = FindCourseByCode(courseCode);

            if (student == null || course == null)
            {
                return false;
            }

            // Kontrollerer at studenten faktisk er meldt opp i kurset.
            if (!course.EnrolledStudents.Any(s =>
                s.Email.Equals(studentEmail, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            course.RemoveStudent(student);
            return true;
        }

        // -------------------------
        // Bibliotekadministrasjon
        // -------------------------

        /// <summary>
        /// Registrerer en ny bok i biblioteket.
        /// </summary>
        public void RegisterBook(Book book)
        {
            if (book != null)
            {
                Books.Add(book);
            }
        }

        /// <summary>
        /// Søker etter bøker basert på tittel.
        /// </summary>
        public IEnumerable<Book> SearchBooks(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return Enumerable.Empty<Book>();
            }

            return Books.Where(b =>
                b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Oppretter et utlån dersom bok og bruker finnes,
        /// og boka er tilgjengelig.
        /// </summary>
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

            // Reduserer antall tilgjengelige eksemplarer etter utlån.
            book.AvailableCopies--;
            return true;
        }

        /// <summary>
        /// Returnerer en bok og oppdaterer lagerstatus.
        /// </summary>
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
            // Øker tilgjengelige eksemplarer når boka leveres tilbake.
            loan.Book.AvailableCopies++;
            return true;
        }

        /// <summary>
        /// Returnerer alle aktive utlån.
        /// </summary>
        public IEnumerable<Loan> GetActiveLoans()
        {
            return Loans.Where(l => l.ReturnDate == null);
        }

        /// <summary>
        /// Returnerer utlånshistorikk sortert etter dato.
        /// </summary>
        public IEnumerable<Loan> GetLoanHistory()
        {
            return Loans.OrderByDescending(l => l.LoanDate);
        }

        /// <summary>
        /// Genererer neste ledige bok-ID.
        /// </summary>
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
