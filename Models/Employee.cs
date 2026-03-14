C#
namespace UniversitySystem.Models
{
    public class Employee : User
    {
        public string EmployeeID { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }

        public Employee(string name, string email, string employeeId, string position, string department)
            : base(name, email)
        {
            EmployeeID = employeeId;
            Position = position;
            Department = department;
        }
    }
}
