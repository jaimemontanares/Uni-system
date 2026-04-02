using System.Collections.Generic;
using System.Linq;

namespace UniversitySystem.Models
{
    /// <summary>
    /// Representerer et kurs ved universitetet.
    /// Inneholder informasjon om kursdetaljer.
    /// </summary>
    public class Course
    {
        // Unik kode for kurset, for eksempel IS-110.
        public string CourseCode { get; set; }
        // Navn på kurset.
        public string CourseName { get; set; }
        // Antall studiepoeng kurset gir.
        public int Credits { get; set; }
        // Maksimalt antall studenter som kan meldes opp.
        public int MaxStudents { get; set; }

        public string LecturerId { get; set; }

        public List<string> Syllabus { get; set; }

        public Course(
            string courseCode,
            string courseName,
            int credits,
            int maxStudents,
            string lecturerId)
        {
            CourseCode = corseCode;
            CourseName = courseName;
            Credits = credits;
            MaxStudents = maxStudents;
            LecturerId = lecturerId;
            Syllasbus = new List<string>();
        }

    }
}
