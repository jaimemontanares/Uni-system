using System;

namespace UniversitySystem.Models
{
    /// <summary>
    /// Representerer et utlån av en bok til en bruker.
    /// Kobler sammen bok, bruker og utlånsperiode.
    /// </summary>

    public class Loan
    {
        public string UserId { get; set; }
        public string BookId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public bool IsActive => ReturnDate == null;

        public Loan(string userId, string bookId)
        {
            UserId = userId;
            BookId = bookId;
            LoanDate = DateTime.Now;
        }

        public void ReturnBook()
        {
            ReturnDate = DateTime.Now;
        }
    }
}
