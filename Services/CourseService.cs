using System;
using System.Collections.Generic;
using System.Linq;
using UniversitySystem.Models;

namespace UniversitySystem.Services
{
    /// <summary>
    /// Serviceklasse som håndterer kurs og kurspåmeldinger.
    /// Ansvar:
    /// - Opprette og søke etter kurs
    /// - Melde studenter på og av kurs
    /// - Hente kurs og påmeldinger
    /// - Registrere pensum
    /// - Sette karakterer
    /// </summary>
    public class CourseService
    {
        // Intern liste over alle kurs i systemet.
        private readonly List<Course> _courses = new();

        // Intern liste over alle påmeldinger mellom studenter og kurs.
        private readonly List<CourseEnrollment> _enrollments = new();

        // Referanse til UserService for å slå opp brukere og kontrollere roller.
        private readonly UserService _userService;

        /// <summary>
        /// Oppretter en ny CourseService.
        /// </summary>
        public CourseService(UserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        /// <summary>
        /// Returnerer alle kurs som en kopi av internlisten.
        /// </summary>
        public List<Course> GetAllCourses()
        {
            return new List<Course>(_courses);
        }

        /// <summary>
        /// Returnerer alle kurspåmeldinger som en kopi av internlisten.
        /// </summary>
        public List<CourseEnrollment> GetAllEnrollments()
        {
            return new List<CourseEnrollment>(_enrollments);
        }

        /// <summary>
        /// Finner et kurs basert på kurskode.
        /// Returnerer null hvis kurset ikke finnes.
        /// </summary>
        public Course? FindCourseByCode(string courseCode)
        {
            if (string.IsNullOrWhiteSpace(courseCode))
            {
                return null;
            }

            return _courses.FirstOrDefault(c =>
                c.CourseCode.Equals(courseCode, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Søker etter kurs basert på kurskode eller kursnavn.
        /// </summary>
        public List<Course> SearchCourses(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return new List<Course>();
            }

            return _courses
                .Where(c =>
                    c.CourseCode.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    c.CourseName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        /// <summary>
        /// Oppretter et nytt kurs dersom input er gyldig og brukeren er faglærer.
        /// </summary>
        public bool CreateCourse(
            string courseCode,
            string courseName,
            int credits,
            int maxStudents,
            string lecturerId,
            out string message)
        {
            if (string.IsNullOrWhiteSpace(courseCode) || string.IsNullOrWhiteSpace(courseName))
            {
                message = "Kurskode og kursnavn må fylles ut.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(lecturerId))
            {
                message = "Faglærer-ID må fylles ut.";
                return false;
            }

            if (credits <= 0)
            {
                message = "Studiepoeng må være større enn 0.";
                return false;
            }

            if (maxStudents <= 0)
            {
                message = "Maks antall studenter må være større enn 0.";
                return false;
            }

            var lecturer = _userService.FindById(lecturerId);
            if (lecturer == null || lecturer.Role != RoleType.Lecturer)
            {
                message = "Kun faglærer kan opprette kurs.";
                return false;
            }

            if (_courses.Any(c => c.CourseCode.Equals(courseCode, StringComparison.OrdinalIgnoreCase)))
            {
                message = "Et kurs med denne kurskoden finnes allerede.";
                return false;
            }

            if (_courses.Any(c => c.CourseName.Equals(courseName, StringComparison.OrdinalIgnoreCase)))
            {
                message = "Et kurs med dette navnet finnes allerede.";
                return false;
            }

            var course = new Course(courseCode, courseName, credits, maxStudents, lecturerId);
            _courses.Add(course);

            message = "Kurset ble opprettet.";
            return true;
        }

        /// <summary>
        /// Melder en student på et kurs dersom studenten og kurset er gyldige.
        /// </summary>
        public bool EnrollStudent(string studentId, string courseCode, out string message)
        {
            if (string.IsNullOrWhiteSpace(studentId) || string.IsNullOrWhiteSpace(courseCode))
            {
                message = "Student-ID og kurskode må fylles ut.";
                return false;
            }

            var user = _userService.FindById(studentId);
            if (user == null || (user.Role != RoleType.Student && user.Role != RoleType.ExchangeStudent))
            {
                message = "Kun studenter kan meldes på kurs.";
                return false;
            }

            var course = FindCourseByCode(courseCode);
            if (course == null)
            {
                message = "Kurset finnes ikke.";
                return false;
            }

            bool alreadyEnrolled = _enrollments.Any(e =>
                e.StudentId == studentId &&
                e.CourseCode.Equals(courseCode, StringComparison.OrdinalIgnoreCase));

            if (alreadyEnrolled)
            {
                message = "Studenten er allerede meldt på kurset.";
                return false;
            }

            int currentEnrollmentCount = _enrollments.Count(e =>
                e.CourseCode.Equals(courseCode, StringComparison.OrdinalIgnoreCase));

            if (currentEnrollmentCount >= course.MaxStudents)
            {
                message = "Kurset er fullt.";
                return false;
            }

            _enrollments.Add(new CourseEnrollment(studentId, courseCode));
            message = "Studenten ble meldt på kurset.";
            return true;
        }

        /// <summary>
        /// Melder en student av et kurs.
        /// </summary>
        public bool UnenrollStudent(string studentId, string courseCode, out string message)
        {
            if (string.IsNullOrWhiteSpace(studentId) || string.IsNullOrWhiteSpace(courseCode))
            {
                message = "Student-ID og kurskode må fylles ut.";
                return false;
            }

            var enrollment = _enrollments.FirstOrDefault(e =>
                e.StudentId == studentId &&
                e.CourseCode.Equals(courseCode, StringComparison.OrdinalIgnoreCase));

            if (enrollment == null)
            {
                message = "Studenten er ikke meldt på dette kurset.";
                return false;
            }

            _enrollments.Remove(enrollment);
            message = "Studenten ble meldt av kurset.";
            return true;
        }

        /// <summary>
        /// Henter alle kurs en bestemt student er meldt på.
        /// </summary>
        public List<Course> GetCoursesForStudent(string studentId)
        {
            var courseCodes = _enrollments
                .Where(e => e.StudentId == studentId)
                .Select(e => e.CourseCode)
                .ToList();

            return _courses
                .Where(c => courseCodes.Contains(c.CourseCode))
                .ToList();
        }

        /// <summary>
        /// Henter alle kurs en bestemt faglærer underviser i.
        /// </summary>
        public List<Course> GetCoursesForLecturer(string lecturerId)
        {
            return _courses
                .Where(c => c.LecturerId == lecturerId)
                .ToList();
        }

        /// <summary>
        /// Henter alle påmeldinger for en student.
        /// </summary>
        public List<CourseEnrollment> GetEnrollmentsForStudent(string studentId)
        {
            return _enrollments
                .Where(e => e.StudentId == studentId)
                .ToList();
        }

        /// <summary>
        /// Henter alle påmeldinger for et kurs.
        /// </summary>
        public List<CourseEnrollment> GetEnrollmentsForCourse(string courseCode)
        {
            return _enrollments
                .Where(e => e.CourseCode.Equals(courseCode, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        /// <summary>
        /// Legger til et pensumpunkt på et kurs dersom faglæreren eier kurset.
        /// </summary>
        public bool AddSyllabusToCourse(
            string lecturerId,
            string courseCode,
            string syllabusItem,
            out string message)
        {
            if (string.IsNullOrWhiteSpace(lecturerId) || string.IsNullOrWhiteSpace(courseCode))
            {
                message = "Faglærer-ID og kurskode må fylles ut.";
                return false;
            }

            var course = FindCourseByCode(courseCode);
            if (course == null)
            {
                message = "Kurset finnes ikke.";
                return false;
            }

            if (course.LecturerId != lecturerId)
            {
                message = "Du kan bare registrere pensum på egne kurs.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(syllabusItem))
            {
                message = "Pensum kan ikke være tomt.";
                return false;
            }

            bool syllabusExists = course.Syllabus.Any(item =>
                item.Equals(syllabusItem, StringComparison.OrdinalIgnoreCase));

            if (syllabusExists)
            {
                message = "Dette pensumpunktet finnes allerede.";
                return false;
            }

            course.Syllabus.Add(syllabusItem);
            message = "Pensum registrert.";
            return true;
        }

        /// <summary>
        /// Setter karakter på en student i et kurs dersom faglæreren eier kurset.
        /// </summary>
        public bool SetGrade(
            string lecturerId,
            string studentId,
            string courseCode,
            string grade,
            out string message)
        {
            if (string.IsNullOrWhiteSpace(lecturerId) ||
                string.IsNullOrWhiteSpace(studentId) ||
                string.IsNullOrWhiteSpace(courseCode) ||
                string.IsNullOrWhiteSpace(grade))
            {
                message = "Faglærer-ID, student-ID, kurskode og karakter må fylles ut.";
                return false;
            }

            var course = FindCourseByCode(courseCode);
            if (course == null)
            {
                message = "Kurset finnes ikke.";
                return false;
            }

            if (course.LecturerId != lecturerId)
            {
                message = "Du kan bare sette karakter i egne kurs.";
                return false;
            }

            var enrollment = _enrollments.FirstOrDefault(e =>
                e.StudentId == studentId &&
                e.CourseCode.Equals(courseCode, StringComparison.OrdinalIgnoreCase));

            if (enrollment == null)
            {
                message = "Studenten er ikke meldt på kurset.";
                return false;
            }

            enrollment.Grade = grade;
            message = "Karakter satt.";
            return true;
        }
    }
}
