namespace UniversitySystem.Models
{
    /// <summary>
    /// Representerer en ansatt ved universitetet.
    /// Denne klassen brukes som base for faglærer og bibliotekar.
    /// </summary>
    public class Employee : User
    {
        /// <summary>
        /// Ansattnummer for den ansatte.
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// Avdeling den ansatte tilhører.
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Stilling eller rollebeskrivelse.
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Oppretter en ny ansatt.
        /// </summary>
        public Employee(
            string id,
            string employeeId,
            string name,
            string email,
            string username,
            string password,
            string department,
            string position,
            RoleType role)
            : base(id, name, email, username, password, role)
        {
            EmployeeId = employeeId;
            Department = department;
            Position = position;
        }
    }
}
