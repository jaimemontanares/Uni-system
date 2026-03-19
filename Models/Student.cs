using System.Collections.Generic;

namespace UniversitySystem.Models
{
    /// <summary>
    /// Representerer en student i systemet.
    /// Arver grunnleggende brukerdata fra User og inneholder studentspesifikke egenskaper.
    /// </summary>
    public class Student : User
    {
        // Unik identifikator for studenten.
        public string StudentID { get; set; }
         // Liste over kurs studenten er meldt opp i.
        public List<Course> EnrolledCourses { get; set; } = new List<Course>();

        /// <summary>
        /// Oppretter en ny student med navn, e-post og studentnummer.
        /// </summary>
        /// <param name="name">Studentens navn.</param>
        /// <param name="email">Studentens e-postadresse.</param>
        /// <param name="studentId">Studentens unike ID.</param>
        public Student(string name, string email, string studentId)
            : base(name, email)
        {
            StudentID = studentId;
        }
    }
}
