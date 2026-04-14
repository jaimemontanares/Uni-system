namespace UniversitySystem.Models
{
    /// <summary>
    /// Representerer en bok i biblioteksystemet.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Unik ID for boken.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Bokas tittel.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Bokas forfatter.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Totalt antall eksemplarer av boken.
        /// </summary>
        public int TotalCopies { get; set; }

        /// <summary>
        /// Antall tilgjengelige eksemplarer som kan lånes.
        /// </summary>
        public int AvailableCopies { get; set; }

        /// <summary>
        /// Oppretter en ny bok.
        /// </summary>
        public Book(string id, string title, string author, int totalCopies)
        {
            Id = id;
            Title = title;
            Author = author;
            TotalCopies = totalCopies;
            AvailableCopies = totalCopies;
        }
    }
}
