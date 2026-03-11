C#
using System;
using System.Collections.Generic; //https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic?view=net-10.0

namespace UniversitySystem
{
    //USER HiERARCHY
    public abstract class User
    {
        public string Name { get; set; }
        public string Email {get; set; }

        protected User(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public override string ToString() => $"{Name} ({Email})"; //kommentar mangler
    }

    public class Student : User
    {
        public string StudentID { get; set; }
        public List<Course> EnrolledCourses { get; set; } = new List<Course>();

        public Student(string name, string email, string studentId) : base(name, email)
        {
            StudentID = studentId;
        }
    }

    public class ExchangeStudent : Student
    {
        public string HomeUniversity { get; set; }
        public string Country { get; set; }
        public DateTime ExchangePeriodeFrom { get; set; }
        public DateTime ExchangePeriodTo { get; set; }

        public Employee(string name, string email, string employeeId, string position, string dept) : base(name, email)
        {
            EmployeeID = employeeId;
            Position = position;
            Department = dept;
        }
    }

    // COURSE
    public class Course
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public int MaxStudents { get; set; }
        public List<Student> EnrolledStudents { get; set; } = new List<Student>();

        public bool EnrollStudent(Student student)
        {
            if (EnrolledStudents.Count < MaxStudents)
            {
                EnrolledStudents.Add(student);
                student.EnrolledCourses.Add(this);
                return true;
            }
            return false;
        }

        public void RemoveStudent(Student student)
        {
            EnrolledStudents.Remove(student);
            student.EnrolledCourses.Remove(this);
        }
    }

}
