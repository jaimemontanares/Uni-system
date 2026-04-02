namespace UniversitySystem.Models
{
    /// <summary>
    /// Representerer en bok i bibliotekssystemet.
    /// Inneholder informasjon om bokdetaljer og tilgjengelige eksemplarer.
    /// </summary>
    public class Book
    {
        // Unik identifikator for boka.
        public string Id { get; set; }
        // Boktittel.
        public string Title { get; set; }
        // Forfatter av boka.
        public string Author { get; set; }
        // Utgivelsesår.
        public int Year { get; set; }
        // Totalt antall eksemplarer registrert i systemet.
        public int TotalCopies { get; set; }
        // Antall eksemplarer som er tilgjengelige for utlån akkurat nå.
        public int AvailableCopies { get; set; }

        public Book(string id, string title, string author, int totalCopies) // mangler kommentar 02.04.26
        {
            Id = id;
            Title = title;
            Author = author;
            TotalCopies = totalCopies;
            AvailableCopies = totalCopies;
        }
    }
}
