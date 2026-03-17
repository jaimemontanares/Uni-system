using System.Collections.Generic;

namespace UniversitySystem.Models
{
    public class Student : User
    {
        public string StudentID { get; set; }
        public List<Course> EnrolledCourses { get; set; } = new List<Course>();

        public Student(string name, string email, string studentId)
            : base(name, email)
        {
            StudentID = studentId;
        }
    }
}
