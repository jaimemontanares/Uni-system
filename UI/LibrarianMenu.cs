using System;
using System.Linq;
using UniversitySystem.Models;
using UniversitySystem.Services;

namespace UniversitySystem.UI
{
    /// <summary>
    /// Meny for bibliotekar.
    /// Håndterer bokregistrering, bokoversikt og utlånshistorikk.
    /// </summary>
    public class LibrarianMenu
    {
        private readonly UniversityManager _manager;
        private readonly User _currentUser;

        /// <summary>
        /// Oppretter en ny bibliotekarmeny.
        /// </summary>
        /// <param name="manager">Systemets tjenestesamler.</param>
        /// <param name="currentUser">Innlogget bruker.</param>
        public LibrarianMenu(UniversityManager manager, User currentUser)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        /// <summary>
        /// Starter hovedløkken for bibliotekarmenyen.
        /// </summary>
        public void Run()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine($"=== BIBLIOTEKARMENY | {_currentUser.Name} ===");
                Console.WriteLine("[1] Registrer ny bok");
                Console.WriteLine("[2] Se alle bøker");
                Console.WriteLine("[3] Søk etter bok");
                Console.WriteLine("[4] Se aktive utlån");
                Console.WriteLine("[5] Se all lånehistorikk");
                Console.WriteLine("[0] Logg ut");

                int choice = InputHelper.ReadMenuChoice(0, 5);

                switch (choice)
                {
                    case 1:
                        RegisterBook();
                        break;

                    case 2:
                        ShowAllBooks();
                        break;

                    case 3:
                        SearchBooks();
                        break;

                    case 4:
                        ShowActiveLoans();
                        break;

                    case 5:
                        ShowLoanHistory();
                        break;

                    case 0:
                        running = false;
                        break;
                }
            }
        }

        /// <summary>
        /// Registrerer en ny bok i biblioteket.
        /// </summary>
        private void RegisterBook()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTRER NY BOK ===");

            string bookId = InputHelper.ReadRequiredString("Bok-ID: ");
            string title = InputHelper.ReadRequiredString("Tittel: ");
            string author = InputHelper.ReadRequiredString("Forfatter: ");
            int totalCopies = InputHelper.ReadInt("Antall eksemplarer: ");

            bool success = _manager.LibraryService.RegisterBook(
                id: bookId,
                title: title,
                author: author,
                totalCopies: totalCopies,
                librarianId: _currentUser.Id,
                out string message);

            Console.WriteLine(message);
            InputHelper.Pause();
        }

        /// <summary>
        /// Viser alle registrerte bøker i biblioteket.
        /// </summary>
        private void ShowAllBooks()
        {
            Console.Clear();
            Console.WriteLine("=== ALLE BØKER ===");

            var books = _manager.LibraryService.GetAllBooks();

            if (!books.Any())
            {
                Console.WriteLine("Ingen bøker registrert.");
                InputHelper.Pause();
                return;
            }

            foreach (var book in books)
            {
                Console.WriteLine(
                    $"{book.Id} | {book.Title} | {book.Author} | Tilgjengelig: {book.AvailableCopies}/{book.TotalCopies}");
            }

            InputHelper.Pause();
        }

        /// <summary>
        /// Søker etter bøker basert på tittel eller forfatter.
        /// </summary>
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
                Console.WriteLine(
                    $"{book.Id} | {book.Title} | {book.Author} | Tilgjengelig: {book.AvailableCopies}/{book.TotalCopies}");
            }

            InputHelper.Pause();
        }

        /// <summary>
        /// Viser alle bøker som for øyeblikket er utlånt.
        /// </summary>
        private void ShowActiveLoans()
        {
            Console.Clear();
            Console.WriteLine("=== AKTIVE UTLÅN ===");

            var loans = _manager.LibraryService.GetActiveLoans();

            if (!loans.Any())
            {
                Console.WriteLine("Ingen aktive utlån.");
                InputHelper.Pause();
                return;
            }

            foreach (var loan in loans)
            {
                var user = _manager.UserService.FindById(loan.UserId);
                var book = _manager.LibraryService.FindBookById(loan.BookId);

                string borrowerName = user?.Name ?? "Ukjent bruker";
                string bookTitle = book?.Title ?? "Ukjent bok";

                Console.WriteLine(
                    $"Bruker: {borrowerName} ({loan.UserId}) | Bok: {bookTitle} ({loan.BookId}) | Utlånt: {loan.LoanDate:dd.MM.yyyy HH:mm}");
            }

            InputHelper.Pause();
        }

        /// <summary>
        /// Viser full lånehistorikk, inkludert leverte bøker.
        /// </summary>
        private void ShowLoanHistory()
        {
            Console.Clear();
            Console.WriteLine("=== ALL LÅNEHISTORIKK ===");

            var loans = _manager.LibraryService.GetAllLoans();

            if (!loans.Any())
            {
                Console.WriteLine("Ingen lånehistorikk funnet.");
                InputHelper.Pause();
                return;
            }

            foreach (var loan in loans.OrderByDescending(l => l.LoanDate))
            {
                var user = _manager.UserService.FindById(loan.UserId);
                var book = _manager.LibraryService.FindBookById(loan.BookId);

                string borrowerName = user?.Name ?? "Ukjent bruker";
                string bookTitle = book?.Title ?? "Ukjent bok";
                string returnText = loan.ReturnDate.HasValue
                    ? loan.ReturnDate.Value.ToString("dd.MM.yyyy HH:mm")
                    : "Ikke levert";

                Console.WriteLine(
                    $"Bruker: {borrowerName} ({loan.UserId}) | Bok: {bookTitle} ({loan.BookId}) | Utlånt: {loan.LoanDate:dd.MM.yyyy HH:mm} | Returnert: {returnText}");
            }

            InputHelper.Pause();
        }
    }
}
