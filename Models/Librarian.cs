namespace UniversitySystem.Models
{
    /// <summary>
    /// Representerer en bibliotekar ved universitetet.
    /// </summary>
    public class Librarian : Employee
    {
        /// <summary>
        /// Oppretter en ny bibliotekar.
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
                department,
                "Bibliotekar",
                RoleType.Librarian)
        {
        }
    }
}
