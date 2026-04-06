using System.Collections.Generic;

namespace UniversitySystem.Models
{
    /// <summary>
    /// Representerer en student i systemet.
    /// </summary>
    public class Student : User
    {
        /// <summary>
        /// Studentens unike studentnummer.
        /// </summary>
        public string StudentId { get; set; }

        /// <summary>
        /// Liste over kurskoder studenten er meldt på.
        /// Denne brukes som enkel oversikt i modellen.
        /// Selve koblingen mellom student og kurs håndteres i CourseService.
        /// </summary>
        public List<string> EnrolledCourses { get; set; } = new();

        /// <summary>
        /// Oppretter en ny student.
        /// </summary>
        public Student(
            string id,
            string studentId,
            string name,
            string email,
            string username,
            string password)
            : base(id, name, email, username, password, RoleType.Student)
        {
            StudentId = studentId;
        }
    }
}
