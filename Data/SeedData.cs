using UniversitySystem.Models;
using UniversitySystem.Services;

namespace UniversitySystem.Data
{
    /// <summary>
    /// Oppretter eksempeldata slik at systemet kan testes ved oppstart.
    /// </summary>
    public static class SeedData
    {
        /// <summary>
        /// Initialiserer systemet med brukere, bøker og kurs.
        /// </summary>
        public static UniversityManager Initialize()
        {
            var manager = new UniversityManager();
            
            // Legger inn eksempelbrukere.
            manager.Users.Add(new Student("Alice Smith", "alice@uni.com", "S101"));
            manager.Users.Add(new ExchangeStudent("Maria Lopez", "maria@uni.com", "S102", "University of Madrid", "Spain"));
            manager.Users.Add(new Employee("Dr. Bob", "bob@uni.com", "E001", "Lecturer", "CS"));

            // Registrerer eksempelbøker i biblioteket.
            manager.Books.Add(new Book
            {
                Id = 1,
                Title = "C# in Depth",
                Author = "Jon Skeet",
                Year = 2019,
                TotalCopies = 2,
                AvailableCopies = 2
            });

            manager.Books.Add(new Book
            {
                Id = 2,
                Title = "Clean Code",
                Author = "Robert C. Martin",
                Year = 2008,
                TotalCopies = 3,
                AvailableCopies = 3
            });

            // Oppretter eksempelkurser.
            manager.Courses.Add(new Course
            {
                CourseCode = "CS101",
                CourseName = "Intro to Programming",
                Credits = 10,
                MaxStudents = 30
            });

            manager.Courses.Add(new Course
            {
                CourseCode = "CS202",
                CourseName = "Object-Oriented Programming",
                Credits = 10,
                MaxStudents = 25
            });

            return manager;
        }
    }
}
