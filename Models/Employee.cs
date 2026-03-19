namespace UniversitySystem.Models
{
    /// <summary>
    /// Representerer en ansatt ved universitetet.
    /// Arver grunnleggende brukerdata fra User og inneholder informasjon om stilling og avdeling.
    /// </summary>
    public class Employee : User
    {
        // Unik identifikator for ansatte i systemet.
        public string EmployeeID { get; set; }
        // Ansattes rolle eller stilling ved universitetet.
        public string Position { get; set; }
        // Avdelingen den ansatte tilhører.
        public string Department { get; set; }
        
        /// <summary>
        /// Oppretter en ny ansatt med navn, e-post, ansatt-ID, stilling og avdeling.
        /// </summary>
        /// <param name="name">Navn på den ansatte.</param>
        /// <param name="email">E-postadresse.</param>
        /// <param name="employeeId">Ansattens unike ID.</param>
        /// <param name="position">Stillingsbetegnelse.</param>
        /// <param name="department">Avdelingstilknytning.</param>
        public Employee(string name, string email, string employeeId, string position, string department)
            : base(name, email)
        {
            EmployeeID = employeeId;
            Position = position;
            Department = department;
        }
    }
}
