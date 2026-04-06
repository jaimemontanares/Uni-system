namespace UniversitySystem.Models
{
    /// <summary>
    /// Representerer en bibliotekansatt.
    /// </summary>
    public class Librarian : Employee
    {
        /// <summary>
        /// Oppretter en ny bibliotekansatt.
        /// </summary>
        public Librarian(
            string id,
            string employeeId,
            string name,
            string email,
            string username,
            string password,
            string department)
            : base(
                id,
                employeeId,
                name,
                email,
                username,
                password,
                RoleType.Librarian,
                "Bibliotekansatt",
                department)
        {
        }
    }
}
