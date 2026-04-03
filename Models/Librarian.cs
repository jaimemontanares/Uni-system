namespace UniversitySystem.Models
{
  public class Librarian : Employee
  {
    public Librarian(
      string id,
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
  }
}
