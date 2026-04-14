using UniversitySystem.Models;
using UniversitySystem.Services;

namespace UniversitySystem.UI
{
    /// <summary>
    /// Applikasjonens hovedmeny.
    /// Håndterer oppstart, innlogging, registrering og videresending til riktig rollemeny.
    /// </summary>
    public class Menu
    {
        private readonly UniversityManager _manager;

        public Menu(UniversityManager manager)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== UNIVERSITY MANAGEMENT SYSTEM ===");
                Console.WriteLine("[1] Jeg er eksisterende bruker");
                Console.WriteLine("[2] Jeg er ny bruker");
                Console.WriteLine("[0] Avslutt");

                int choice = InputHelper.ReadMenuChoice(0, 2);

                switch (choice)
                {
                    case 1:
                        HandleLogin();
                        break;

                    case 2:
                        HandleRegistration();
                        break;

                    case 0:
                        running = false;
                        break;
                }
            }
        }

        private void HandleLogin()
        {
            Console.Clear();
            Console.WriteLine("=== LOGG INN ===");

            string username = InputHelper.ReadRequiredString("Brukernavn: ");
            string password = InputHelper.ReadRequiredString("Passord: ");

            User? user = _manager.AuthService.Login(username, password);

            if (user == null)
            {
                Console.WriteLine("Innlogging mislyktes. Sjekk brukernavn og passord.");
                InputHelper.Pause();
                return;
            }

            RouteUser(user);
        }

        private void HandleRegistration()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTRER NY STUDENT ===");

            string name = InputHelper.ReadRequiredString("Navn: ");
            string email = InputHelper.ReadRequiredString("E-post: ");
            string username = InputHelper.ReadRequiredString("Ønsket brukernavn: ");
            string password = InputHelper.ReadRequiredString("Ønsket passord: ");

            string id = $"U{_manager.UserService.GetAllUsers().Count + 1}";
            string studentId = $"S{1000 + _manager.UserService.GetAllUsers().Count + 1}";

            bool success = _manager.AuthService.RegisterStudent(
                id,
                studentId,
                name,
                email,
                username,
                password,
                out string message);

            Console.WriteLine(message);
            InputHelper.Pause();
        }

        private void RouteUser(User user)
        {
            switch (user.Role)
            {
                case RoleType.Student:
                case RoleType.ExchangeStudent:
                    new StudentMenu(_manager, user).Run();
                    break;

                case RoleType.Lecturer:
                    new LecturerMenu(_manager, user).Run();
                    break;

                case RoleType.Librarian:
                    new LibrarianMenu(_manager, user).Run();
                    break;

                default:
                    Console.WriteLine("Denne rollen har foreløpig ingen egen meny.");
                    InputHelper.Pause();
                    break;
            }
        }
    }
}
