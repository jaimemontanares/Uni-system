using UniversitySystem.Models;
using UniversitySystem.Services;

namespace UniversitySystem.UI
{
    /// <summary>
    /// Meny for bibliotekansatt.
    /// Håndterer bokregistrering, søk og oversikt over lån.
    /// </summary>
    public class LibrarianMenu
    {
        private readonly UniversityManager _manager;
        private readonly User _currentUser;

        public LibrarianMenu(UniversityManager manager, User currentUser)
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
                Console.WriteLine($"=== BIBLIOTEKMENY | {_currentUser.Name} ===");
                Console.WriteLine("[1] Registrer bok");
                Console.WriteLine("[2] Søk etter bok");
                Console.WriteLine("[3] Se aktive lån");
                Console.WriteLine("[4] Se lånehistorikk");
                Console.WriteLine("[0] Logg ut");

                int choice = InputHelper.ReadMenuChoice(0, 4);

                switch (choice)
                {
                    case 1:
                        RegisterBook();
                        break;
                    case 2:
                        SearchBooks();
                        break;
                    case 3:
                        ShowActiveLoans();
                        break;
                    case 4:
                        ShowLoanHistory();
                        break;
                    case 0:
                        running = false;
                        break;
                }
            }
        }

        private void RegisterBook()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTRER BOK ===");

            string id = $"B{_manager.LibraryService.GetAllBooks().Count + 1}";
            string title = InputHelper.ReadRequiredString("Tittel: ");
            string author = InputHelper.ReadRequiredString("Forfatter: ");
            int copies = InputHelper.ReadInt("Antall eksemplarer: ");

            bool success = _manager.LibraryService.RegisterBook(
                id,
                title,
                author,
                copies,
                _currentUser.Id,
                out string message);

            Console.WriteLine(message);
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

        private void ShowActiveLoans()
        {
            Console.Clear();
            Console.WriteLine("=== AKTIVE LÅN ===");

            var loans = _manager.LibraryService.GetActiveLoans();

            if (!loans.Any())
            {
                Console.WriteLine("Ingen aktive lån.");
                InputHelper.Pause();
                return;
            }

            foreach (var loan in loans)
            {
                Console.WriteLine($"Bruker-ID: {loan.UserId} | Bok-ID: {loan.BookId} | Utlånsdato: {loan.LoanDate:g}");
            }

            InputHelper.Pause();
        }

        private void ShowLoanHistory()
        {
            Console.Clear();
            Console.WriteLine("=== LÅNEHISTORIKK ===");

            var loans = _manager.LibraryService.GetLoanHistory();

            if (!loans.Any())
            {
                Console.WriteLine("Ingen lånehistorikk funnet.");
                InputHelper.Pause();
                return;
            }

            foreach (var loan in loans)
            {
                string status = loan.IsActive ? "Aktivt lån" : $"Returnert: {loan.ReturnDate:g}";
                Console.WriteLine($"Bruker-ID: {loan.UserId} | Bok-ID: {loan.BookId} | Lånt: {loan.LoanDate:g} | {status}");
            }

            InputHelper.Pause();
        }
    }
}
