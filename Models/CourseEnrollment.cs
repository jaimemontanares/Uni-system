using System;

namespace UniversitySystem.Models
{
    /// <summary>
    /// Representerer en påmelding mellom student og kurs.
    /// </summary>
    public class CourseEnrollment
    {
        /// <summary>
        /// Intern bruker-ID til studenten.
        /// </summary>
        public string StudentId { get; set; }

        /// <summary>
        /// Kurskoden studenten er meldt på.
        /// </summary>
        public string CourseCode { get; set; }

        /// <summary>
        /// Tidspunktet påmeldingen ble registrert.
        /// </summary>
        public DateTime EnrollmentDate { get; set; }

        /// <summary>
        /// Karakteren studenten har fått i kurset.
        /// Null betyr at karakter ikke er satt ennå.
        /// </summary>
        public string? Grade { get; set; }

        /// <summary>
        /// Oppretter en ny kurspåmelding.
        /// </summary>
        public CourseEnrollment(string studentId, string courseCode)
        {
            StudentId = studentId;
            CourseCode = courseCode;
            EnrollmentDate = DateTime.Now;
        }
    }
}
