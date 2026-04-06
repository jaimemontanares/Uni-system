nnamespace UniversitySystem.Models
{
    /// <summary>
    /// Representerer en bok i biblioteket.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Unik ID for boka.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Boktittel.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Forfatter.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Totalt antall eksemplarer av boka.
        /// </summary>
        public int TotalCopies { get; set; }

        /// <summary>
        /// Antall eksemplarer som er tilgjengelige akkurat nå.
        /// </summary>
        public int AvailableCopies { get; set; }

        /// <summary>
        /// Oppretter en ny bok og setter tilgjengelige eksemplarer lik totalen.
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
