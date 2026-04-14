using System.Collections.Generic;

namespace UniversitySystem.Models
{
    /// <summary>
    /// Representerer en student ved universitetet.
    /// </summary>
    public class Student : User
    {
        /// <summary>
        /// Studentens studentnummer.
        /// </summary>
        public string StudentId { get; set; }

        /// <summary>
        /// Liste over kurskoder studenten er meldt opp i.
        /// Denne listen kan brukes som enkel oversikt i modellen,
        /// selv om den faktiske koblingen også håndteres i CourseService.
        /// </summary>
        public List<string> EnrolledCourseCodes { get; set; }

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
            EnrolledCourseCodes = new List<string>();
        }
    }
}
