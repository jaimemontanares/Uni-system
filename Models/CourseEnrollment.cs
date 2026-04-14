namespace UniversitySystem.Models
{
    /// <summary>
    /// Representerer en påmelding av en student til et kurs.
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
        /// Eventuell karakter studenten har fått i kurset.
        /// </summary>
        public string? Grade { get; set; }

        /// <summary>
        /// Oppretter en ny kurspåmelding.
        /// </summary>
        public CourseEnrollment(string studentId, string courseCode)
        {
            StudentId = studentId;
            CourseCode = courseCode;
            Grade = null;
        }
    }
}
