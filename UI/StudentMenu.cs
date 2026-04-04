using UniversitySystem.Models;
using UniversitySystem.Services;

namespace UniversitySystem.UI
{
    /// <summary>
    /// Meny for student og utvekslingsstudent.
    /// Viser kun funksjoner som tilhører studentrollen.
    /// </summary>
    public class StudentMenu
    {
        private readonly UniversityManager _manager;
        private readonly User _currentUser;

        public StudentMenu(UniversityManager manager, User currentUser)
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
                Console.WriteLine($"=== STUDENTMENY | {_currentUser.Name} ===");
                Console.WriteLine("[1] Se alle kurs");
                Console.WriteLine("[2] Meld deg på kurs");
                Console.WriteLine("[3] Meld deg av kurs");
                Console.WriteLine("[4] Se mine kurs");
                Console.WriteLine("[5] Se mine karakterer");
                Console.WriteLine("[6] Søk etter bok");
                Console.WriteLine("[7] Lån bok");
                Console.WriteLine("[8] Lever bok tilbake");
                Console.WriteLine("[0] Logg ut");

                int choice = InputHelper.ReadMenuChoice(0, 8);

                switch (choice)
                {
                    case 1:
                        ShowAllCourses();
                        break;
                    case 2:
                        EnrollInCourse();
                        break;
                    case 3:
                        UnenrollFromCourse();
                        break;
                    case 4:
                        ShowMyCourses();
                        break;
                    case 5:
                        ShowMyGrades();
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

        private void ShowAllCourses()
        {
            Console.Clear();
            Console.WriteLine("=== ALLE KURS ===");

            var courses = _manager.CourseService.GetAllCourses();

            if (!courses.Any())
            {
                Console.WriteLine("Ingen kurs registrert.");
                InputHelper.Pause();
                return;
            }

            foreach (var course in courses)
            {
                Console.WriteLine($"{course.Code} | {course.Name} | Studiepoeng: {course.Credits} | Maks: {course.MaxCapacity}");
            }

            InputHelper.Pause();
        }

        private void EnrollInCourse()
        {
            Console.Clear();
            Console.WriteLine("=== MELD DEG PÅ KURS ===");

            string courseCode = InputHelper.ReadRequiredString("Kurskode: ");

            bool success = _manager.CourseService.EnrollStudent(_currentUser.Id, courseCode, out string message);

            Console.WriteLine(message);
            InputHelper.Pause();
        }

        private void UnenrollFromCourse()
        {
            Console.Clear();
            Console.WriteLine("=== MELD DEG AV KURS ===");

            string courseCode = InputHelper.ReadRequiredString("Kurskode: ");

            bool success = _manager.CourseService.UnenrollStudent(_currentUser.Id, courseCode, out string message);

            Console.WriteLine(message);
            InputHelper.Pause();
        }

        private void ShowMyCourses()
        {
            Console.Clear();
            Console.WriteLine("=== MINE KURS ===");

            var courses = _manager.CourseService.GetCoursesForStudent(_currentUser.Id);

            if (!courses.Any())
            {
                Console.WriteLine("Du er ikke meldt på noen kurs.");
                InputHelper.Pause();
                return;
            }

            foreach (var course in courses)
            {
                Console.WriteLine($"{course.Code} | {course.Name}");
            }

            InputHelper.Pause();
        }

        private void ShowMyGrades()
        {
            Console.Clear();
            Console.WriteLine("=== MINE KARAKTERER ===");

            var enrollments = _manager.CourseService.GetEnrollmentsForStudent(_currentUser.Id);

            if (!enrollments.Any())
            {
                Console.WriteLine("Ingen påmeldinger funnet.");
                InputHelper.Pause();
                return;
            }

            foreach (var enrollment in enrollments)
            {
                string gradeText = string.IsNullOrWhiteSpace(enrollment.Grade) ? "Ikke satt" : enrollment.Grade;
                Console.WriteLine($"{enrollment.CourseCode} | Karakter: {gradeText}");
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
