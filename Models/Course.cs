using System.Collections.Generic;

namespace UniversitySystem.Models
{
    /// <summary>
    /// Representerer et kurs ved universitetet.
    /// </summary>
    public class Course
    {
        /// <summary>
        /// Unik kurskode, for eksempel IS-110.
        /// </summary>
        public string CourseCode { get; set; }

        /// <summary>
        /// Navn på kurset.
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// Antall studiepoeng kurset gir.
        /// </summary>
        public int Credits { get; set; }

        /// <summary>
        /// Maksimalt antall studenter som kan meldes på.
        /// </summary>
        public int MaxStudents { get; set; }

        /// <summary>
        /// Intern bruker-ID til faglæreren som eier kurset.
        /// </summary>
        public string LecturerId { get; set; }

        /// <summary>
        /// Enkel liste over pensumpunkter for kurset.
        /// </summary>
        public List<string> Syllabus { get; set; }

        /// <summary>
        /// Oppretter et nytt kurs.
        /// </summary>
        public Course(
            string courseCode,
            string courseName,
            int credits,
            int maxStudents,
            string lecturerId)
        {
            CourseCode = courseCode;
            CourseName = courseName;
            Credits = credits;
            MaxStudents = maxStudents;
            LecturerId = lecturerId;
            Syllabus = new List<string>();
        }
    }
}
