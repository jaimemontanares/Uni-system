namespace UniversitySystem.Models
{
  public class Lecturer : Employee
    {
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
        department)
    }
}
