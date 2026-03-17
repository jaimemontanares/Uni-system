using System.Collections.Generic;
using System.Linq;

namespace UniversitySystem.Models
{
    public class Course
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public int MaxStudents { get; set; }
        public List<Student> EnrolledStudents { get; set; } = new List<Student>();

        public bool EnrollStudent(Student student)
        {
            if (student == null)
            {
                return false;
            }

            if (EnrolledStudents.Any(s => s.Email == student.Email))
            {
                return false;
            }

            if (EnrolledStudents.Count >= MaxStudents)
            {
                return false;
            }

            EnrolledStudents.Add(student);

            if (!student.EnrolledCourses.Contains(this))
            {
                student.EnrolledCourses.Add(this);
            }

            return true;
        }

        public void RemoveStudent(Student student)
        {
            if (student == null)
            {
                return;
            }

            EnrolledStudents.Remove(student);
            student.EnrolledCourses.Remove(this);
        }
    }
}
