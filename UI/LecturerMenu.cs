using UniversitySystem.Models;
using UniversitySystem.Services;

namespace UniversitySystem.UI
{
    /// <summary>
    /// Meny for faglærer.
    /// Håndterer kursoppretting, pensum, karaktersetting og biblioteksfunksjoner.
    /// </summary>
    public class LecturerMenu
    {
        private readonly UniversityManager _manager;
        private readonly User _currentUser;

        public LecturerMenu(UniversityManager manager, User currentUser)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine($"=== FAGLÆRERMENY | {_currentUser.Name} ===");
                Console.WriteLine("[1] Opprett kurs");
                Console.WriteLine("[2] Søk etter kurs");
                Console.WriteLine("[3] Registrer pensum");
                Console.WriteLine("[4] Sett karakter");
                Console.WriteLine("[5] Se mine kurs");
                Console.WriteLine("[6] Søk etter bok");
                Console.WriteLine("[7] Lån bok");
                Console.WriteLine("[8] Lever bok tilbake");
                Console.WriteLine("[0] Logg ut");

                int choice = InputHelper.ReadMenuChoice(0, 8);

                switch (choice)
                {
                    case 1:
                        CreateCourse();
                        break;
                    case 2:
                        SearchCourses();
                        break;
                    case 3:
                        AddSyllabus();
                        break;
                    case 4:
                        SetGrade();
                        break;
                    case 5:
                        ShowMyCourses();
                        break;
                    case 6:
                        SearchBooks();
                        break;
                    case 7:
                        BorrowBook();
                        break;
                    case 8:
                        ReturnBook();
                        break;
                    case 0:
                        running = false;
                        break;
                }
            }
        }

        private void CreateCourse()
        {
            Console.Clear();
            Console.WriteLine("=== OPPRETT KURS ===");

            string code = InputHelper.ReadRequiredString("Kurskode: ");
            string name = InputHelper.ReadRequiredString("Kursnavn: ");
            int credits = InputHelper.ReadInt("Studiepoeng: ");
            int maxCapacity = InputHelper.ReadInt("Maks kapasitet: ");

            bool success = _manager.CourseService.CreateCourse(
                code,
                name,
                credits,
                maxCapacity,
                _currentUser.Id,
                out string message);

            Console.WriteLine(message);
            InputHelper.Pause();
        }

        private void SearchCourses()
        {
            Console.Clear();
            Console.WriteLine("=== SØK ETTER KURS ===");

            string searchTerm = InputHelper.ReadRequiredString("Søkeord: ");
            var courses = _manager.CourseService.SearchCourses(searchTerm);

            if (!courses.Any())
            {
                Console.WriteLine("Ingen kurs funnet.");
                InputHelper.Pause();
                return;
            }

            foreach (var course in courses)
            {
                Console.WriteLine($"{course.Code} | {course.Name} | Studiepoeng: {course.Credits}");
            }

            InputHelper.Pause();
        }

        private void AddSyllabus()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTRER PENSUM ===");

            string courseCode = InputHelper.ReadRequiredString("Kurskode: ");
            string syllabusItem = InputHelper.ReadRequiredString("Pensumpunkt: ");

            bool success = _manager.CourseService.AddSyllabusToCourse(
                _currentUser.Id,
                courseCode,
                syllabusItem,
                out string message);

            Console.WriteLine(message);
            InputHelper.Pause();
        }

        private void SetGrade()
        {
            Console.Clear();
            Console.WriteLine("=== SETT KARAKTER ===");

            string courseCode = InputHelper.ReadRequiredString("Kurskode: ");
            string studentId = InputHelper.ReadRequiredString("Student-ID (intern bruker-ID): ");
            string grade = InputHelper.ReadRequiredString("Karakter: ");

            bool success = _manager.CourseService.SetGrade(
                _currentUser.Id,
                studentId,
                courseCode,
                grade,
                out string message);

            Console.WriteLine(message);
            InputHelper.Pause();
        }

        private void ShowMyCourses()
        {
            Console.Clear();
            Console.WriteLine("=== MINE KURS ===");

            var courses = _manager.CourseService.GetCoursesForLecturer(_currentUser.Id);

            if (!courses.Any())
            {
                Console.WriteLine("Du underviser foreløpig ingen kurs.");
                InputHelper.Pause();
                return;
            }

            foreach (var course in courses)
            {
                Console.WriteLine($"{course.Code} | {course.Name}");

                if (course.Syllabus.Any())
                {
                    Console.WriteLine("  Pensum:");
                    foreach (var item in course.Syllabus)
                    {
                        Console.WriteLine($"   - {item}");
                    }
                }
            }

            InputHelper.Pause();
        }

        private void SearchBooks()
        {
            Console.Clear();
            Console.WriteLine("=== SØK ETTER BOK ===");

            string searchTerm = InputHelper.ReadRequiredString("Søkeord (tittel/forfatter): ");
            var books = _manager.LibraryService.SearchBooks(searchTerm);

            if (!books.Any())
            {
                Console.WriteLine("Ingen bøker funnet.");
                InputHelper.Pause();
                return;
            }

            foreach (var book in books)
            {
                Console.WriteLine($"{book.Id} | {book.Title} | {book.Author} | Tilgjengelig: {book.AvailableCopies}/{book.TotalCopies}");
            }

            InputHelper.Pause();
        }

        private void BorrowBook()
        {
            Console.Clear();
            Console.WriteLine("=== LÅN BOK ===");

            string bookId = InputHelper.ReadRequiredString("Bok-ID: ");
            bool success = _manager.LibraryService.BorrowBook(_currentUser.Id, bookId, out string message);

            Console.WriteLine(message);
            InputHelper.Pause();
        }

        private void ReturnBook()
        {
            Console.Clear();
            Console.WriteLine("=== LEVER BOK TILBAKE ===");

            string bookId = InputHelper.ReadRequiredString("Bok-ID: ");
            bool success = _manager.LibraryService.ReturnBook(_currentUser.Id, bookId, out string message);

            Console.WriteLine(message);
            InputHelper.Pause();
        }
    }
}
