using System;
using UniversitySystem.Models;
using UniversitySystem.Services;

namespace UniversitySystem.UI
{
    /// <summary>
    /// Applikasjonens startmeny.
    /// Håndterer oppstart, innlogging, registrering og videresending til riktig rollemeny.
    /// </summary>
    public class StartMenu
    {
        private readonly UniversityManager _manager;

        /// <summary>
        /// Oppretter en ny startmeny.
        /// </summary>
        /// <param name="manager">Systemets tjenestesamler.</param>
        public StartMenu(UniversityManager manager)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>
        /// Starter hovedløkken for applikasjonen.
        /// </summary>
        public void Run()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== UNIVERSITY MANAGEMENT SYSTEM ===");
                Console.WriteLine("[1] Jeg er eksisterende bruker");
                Console.WriteLine("[2] Registrer ny student");
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

        /// <summary>
        /// Håndterer innlogging for eksisterende brukere.
        /// </summary>
        private void HandleLogin()
        {
            Console.Clear();
            Console.WriteLine("=== LOGG INN ===");

            string username = InputHelper.ReadRequiredString("Brukernavn: ");
            string password = InputHelper.ReadRequiredString("Passord: ");

            User? user = _manager.AuthorizationService.Login(username, password);

            if (user == null)
            {
                Console.WriteLine("Innlogging mislyktes. Sjekk brukernavn og passord.");
                InputHelper.Pause();
                return;
            }

            RouteUser(user);
        }

        /// <summary>
        /// Håndterer registrering av ny student.
        /// </summary>
        private void HandleRegistration()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTRER NY STUDENT ===");

            string name = InputHelper.ReadRequiredString("Navn: ");
            string email = InputHelper.ReadRequiredString("E-post: ");
            string username = InputHelper.ReadRequiredString("Ønsket brukernavn: ");
            string password = InputHelper.ReadRequiredString("Ønsket passord: ");

            int nextNumber = _manager.UserService.GetAllUsers().Count + 1;

            string userId = $"U{nextNumber}";
            string studentId = $"S{1000 + nextNumber}";

            bool success = _manager.AuthorizationService.RegisterStudent(
                userId,
                studentId,
                name,
                email,
                username,
                password,
                out string message);

            Console.WriteLine(message);

            if (success)
            {
                Console.WriteLine("Du kan nå logge inn med brukernavnet og passordet du nettopp valgte.");
            }

            InputHelper.Pause();
        }

        /// <summary>
        /// Sender brukeren videre til riktig rollemeny.
        /// </summary>
        /// <param name="user">Innlogget bruker.</param>
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
