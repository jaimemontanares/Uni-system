using System;

namespace UniversitySystem.Models
{
    /// <summary>
    /// Representerer et utlån av en bok til en bruker.
    /// Kobler sammen bok, bruker og utlånsperiode.
    /// </summary>
    public class Loan
    {
        // Boken som er lånt ut.
        public Book Book { get; set; }
        // Brukeren som har lånt boka.
        public User User { get; set; }
        // Dato for når utlånet ble registrert.
        public DateTime LoanDate { get; set; }
        // Returdato settes når boka leveres tilbake.
        // Null betyr at utlånet fortsatt er aktivt.
        public DateTime? ReturnDate { get; set; }
    }
}
