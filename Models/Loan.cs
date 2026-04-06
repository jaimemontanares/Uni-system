using System;

namespace UniversitySystem.Models
{
    /// <summary>
    /// Representerer et utlån av en bok til en bruker.
    /// </summary>
    public class Loan
    {
        /// <summary>
        /// Intern bruker-ID til låneren.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Bok-ID til boka som er lånt ut.
        /// </summary>
        public string BookId { get; set; }

        /// <summary>
        /// Tidspunktet boka ble lånt ut.
        /// </summary>
        public DateTime LoanDate { get; set; }

        /// <summary>
        /// Tidspunktet boka ble levert tilbake.
        /// Null betyr at lånet fortsatt er aktivt.
        /// </summary>
        public DateTime? ReturnDate { get; set; }

        /// <summary>
        /// True når boka fortsatt er ute på lån.
        /// </summary>
        public bool IsActive => ReturnDate == null;

        /// <summary>
        /// Oppretter et nytt lån.
        /// </summary>
        public Loan(string userId, string bookId)
        {
            UserId = userId;
            BookId = bookId;
            LoanDate = DateTime.Now;
        }

        /// <summary>
        /// Marker lånet som returnert.
        /// </summary>
        public void ReturnBook()
        {
            ReturnDate = DateTime.Now;
        }
    }
}
