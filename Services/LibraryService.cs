using UniversitySystem.Models;

namespace UniversitySystem.Services
{
    /// <summary>
    /// Serviceklasse som håndterer bibliotekfunksjonalitet.
    /// Ansvar:
    /// - Registrering av bøker
    /// - Søk etter bøker
    /// - Utlån og innlevering
    /// - Oversikt over aktive lån og lånehistorikk
    /// </summary>
    public class LibraryService
    {
        // Intern liste over alle bøker i biblioteket
        private readonly List<Book> _books = new();

        // Intern liste over alle lån i systemet
        private readonly List<Loan> _loans = new();

        // Referanse til UserService for oppslag og rollekontroll
        private readonly UserService _userService;

        /// <summary>
        /// Oppretter en ny LibraryService.
        /// </summary>
        public LibraryService(UserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        /// <summary>
        /// Returnerer alle bøker som en kopi av internlisten.
        /// </summary>
        public List<Book> GetAllBooks()
        {
            return new List<Book>(_books);
        }

        /// <summary>
        /// Returnerer alle lån som en kopi av internlisten.
        /// </summary>
        public List<Loan> GetAllLoans()
        {
            return new List<Loan>(_loans);
        }

        /// <summary>
        /// Registrerer en ny bok dersom brukeren er bibliotekansatt
        /// og input er gyldig.
        /// </summary>
        public bool RegisterBook(
            string id,
            string title,
            string author,
            int totalCopies,
            string librarianId,
            out string message)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                message = "Bok-ID må fylles ut.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(librarianId))
            {
                message = "Bibliotekar-ID må fylles ut.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
            {
                message = "Tittel og forfatter må fylles ut.";
                return false;
            }

            if (totalCopies <= 0)
            {
                message = "Antall kopier må være større enn 0.";
                return false;
            }

            if (_books.Any(b => b.Id == id))
            {
                message = "En bok med denne ID-en finnes allerede.";
                return false;
            }

            var librarian = _userService.FindById(librarianId);

            if (librarian == null || librarian.Role != RoleType.Librarian)
            {
                message = "Kun bibliotekansatt kan registrere bøker.";
                return false;
            }

            _books.Add(new Book(id, title, author, totalCopies));
            message = "Boken ble registrert.";
            return true;
        }

        /// <summary>
        /// Finner en bok basert på bok-ID.
        /// Returnerer null hvis boken ikke finnes.
        /// </summary>
        public Book? FindBookById(string bookId)
        {
            if (string.IsNullOrWhiteSpace(bookId))
            {
                return null;
            }

            return _books.FirstOrDefault(b => b.Id == bookId);
        }

        /// <summary>
        /// Søker etter bøker basert på tittel eller forfatter.
        /// </summary>
        public List<Book> SearchBooks(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return new List<Book>();
            }

            return _books
                .Where(b =>
                    b.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    b.Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        /// <summary>
        /// Registrerer utlån av bok dersom bruker og bok er gyldige
        /// og det finnes tilgjengelige eksemplarer.
        /// </summary>
        public bool BorrowBook(string userId, string bookId, out string message)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(bookId))
            {
                message = "Bruker-ID og bok-ID må fylles ut.";
                return false;
            }

            var user = _userService.FindById(userId);

            if (user == null)
            {
                message = "Brukeren finnes ikke.";
                return false;
            }

            bool allowedRole =
                user.Role == RoleType.Student ||
                user.Role == RoleType.ExchangeStudent ||
                user.Role == RoleType.Lecturer;

            if (!allowedRole)
            {
                message = "Denne brukeren kan ikke låne bøker.";
                return false;
            }

            var book = FindBookById(bookId);

            if (book == null)
            {
                message = "Boken finnes ikke.";
                return false;
            }

            if (book.AvailableCopies <= 0)
            {
                message = "Ingen tilgjengelige kopier.";
                return false;
            }

            bool alreadyBorrowed = _loans.Any(l =>
                l.UserId == userId &&
                l.BookId == bookId &&
                l.IsActive);

            if (alreadyBorrowed)
            {
                message = "Brukeren har allerede et aktivt lån på denne boken.";
                return false;
            }

            _loans.Add(new Loan(userId, bookId));
            book.AvailableCopies--;

            message = "Boken ble lånt ut.";
            return true;
        }

        /// <summary>
        /// Registrerer innlevering av bok dersom et aktivt lån finnes.
        /// </summary>
        public bool ReturnBook(string userId, string bookId, out string message)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(bookId))
            {
                message = "Bruker-ID og bok-ID må fylles ut.";
                return false;
            }

            var activeLoan = _loans.FirstOrDefault(l =>
                l.UserId == userId &&
                l.BookId == bookId &&
                l.IsActive);

            if (activeLoan == null)
            {
                message = "Ingen aktivt lån ble funnet.";
                return false;
            }

            var book = FindBookById(bookId);

            if (book == null)
            {
                message = "Boken finnes ikke.";
                return false;
            }

            activeLoan.ReturnBook();
            book.AvailableCopies++;

            message = "Boken ble levert tilbake.";
            return true;
        }

        /// <summary>
        /// Returnerer alle aktive lån.
        /// </summary>
        public List<Loan> GetActiveLoans()
        {
            return _loans
                .Where(l => l.IsActive)
                .ToList();
        }

        /// <summary>
        /// Returnerer hele lånehistorikken som en kopi av internlisten.
        /// </summary>
        public List<Loan> GetLoanHistory()
        {
            return new List<Loan>(_loans);
        }

        /// <summary>
        /// Returnerer alle lån for en bestemt bruker.
        /// </summary>
        public List<Loan> GetLoansForUser(string userId)
        {
            return _loans
                .Where(l => l.UserId == userId)
                .ToList();
        }
    }
}
