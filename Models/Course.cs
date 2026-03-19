using System.Collections.Generic;
using System.Linq;

namespace UniversitySystem.Models
{
    /// <summary>
    /// Representerer et kurs ved universitetet.
    /// Inneholder informasjon om kursdetaljer og hvilke studenter som er meldt opp.
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
        // Liste over studenter som er registrert i kurset.
        public List<Student> EnrolledStudents { get; set; } = new List<Student>();

        /// <summary>
        /// Melder en student opp i kurset dersom det er ledig plass
        /// og studenten ikke allerede er registrert.
        /// </summary>
        /// <param name="student">Studenten som skal meldes opp.</param>
        /// <returns>True dersom oppmelding lykkes, ellers false.</returns>
        public bool EnrollStudent(Student student)
        {
            if (student == null)
            {
                return false;
            }

            // Hindrer at samme student meldes opp flere ganger.
            if (EnrolledStudents.Any(s => s.Email == student.Email))
            {
                return false;
            }

            // Sjekker om kurset allerede er fullt.
            if (EnrolledStudents.Count >= MaxStudents)
            {
                return false;
            }

            EnrolledStudents.Add(student);

            // Legger også kurset til studentens kursliste dersom det mangler.
            if (!student.EnrolledCourses.Contains(this))
            {
                student.EnrolledCourses.Add(this);
            }

            return true;
        }

        /// <summary>
        /// Fjerner en student fra kurset og oppdaterer studentens kursliste.
        /// </summary>
        /// <param name="student">Studenten som skal fjernes.</param>
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
