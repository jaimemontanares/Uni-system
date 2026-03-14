C#
using System;

namespace UniversitySystem.Models
{
    public class Loan
    {
        public Book Book { get; set; }
        public User User { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
