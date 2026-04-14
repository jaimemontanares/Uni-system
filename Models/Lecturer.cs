namespace UniversitySystem.Models
{
    /// <summary>
    /// Representerer en faglærer ved universitetet.
    /// </summary>
    public class Lecturer : Employee
    {
        /// <summary>
        /// Oppretter en ny faglærer.
        /// </summary>
        public Lecturer(
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
                "Faglærer",
                RoleType.Lecturer)
        {
        }
    }
}
