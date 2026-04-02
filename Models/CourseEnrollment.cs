namespace UniversitySystem.Models
{
    public class CourseEnrollment
    {
        public string StudentId { get; set; }
        public string CourseCode { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string? Grade { get; set; }

        public CourseEnrollment(string studentId, string courseCode)
        {
            StudentId = studentId;
            CourseCode = courseCode;
            EnrollmentDate = DateTime.Now;
            Grade = null;
        }
    }
}
