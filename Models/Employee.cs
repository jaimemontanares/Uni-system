namespace UniversitySystem.Models
{
    /// <summary>
    /// Representerer en ansatt ved universitetet.
    /// </summary>
    public class Employee : User
    {
        /// <summary>
        /// Ansattens unike ansattnummer.
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// Stillingsbetegnelse, for eksempel faglærer eller bibliotekansatt.
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Avdelingen den ansatte tilhører.
        /// </summary>
        public string Department { get; set; }

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
            RoleType role,
            string position,
            string department)
            : base(id, name, email, username, password, role)
        {
            EmployeeId = employeeId;
            Position = position;
            Department = department;
        }
    }
}
