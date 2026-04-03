using UniversitySystem.Models;

namespace UniversitySystem.Services
{
    /// <summary>
    /// Service for autentisering og registrering av brukere.
    /// Ansvar:
    /// - Registrere nye brukere
    /// - Håndtere innlogging
    /// </summary>
    public class AuthService
    {
        private readonly UserService _userService;

        /// <summary>
        /// Dependency injection av UserService.
        /// </summary>
        public AuthService(UserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        /// <summary>
        /// Registrerer en ny student.
        /// Returnerer false med feilmelding hvis validering feiler.
        /// </summary>
        public bool RegisterStudent(
            string id,
            string studentId,
            string name,
            string email,
            string username,
            string password,
            out string message)
        {
            // Valider input
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                message = "Navn, e-post, brukernavn og passord må fylles ut.";
                return false;
            }

            // Sjekk om brukernavn allerede finnes
            if (_userService.UsernameExists(username))
            {
                message = "Brukernavnet finnes allerede.";
                return false;
            }

            var student = new Student(id, studentId, name, email, username, password);

            // Viktig: bruk returverdi fra UserService
            bool success = _userService.AddUser(student);

            if (!success)
            {
                message = "Kunne ikke registrere bruker.";
                return false;
            }

            message = "Student registrert.";
            return true;
        }

        /// <summary>
        /// Logger inn en bruker basert på brukernavn og passord.
        /// Returnerer null hvis login feiler.
        /// </summary>
        public User? Login(string username, string password)
        {
            // Enkel input-validering
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            var user = _userService.FindByUsername(username);

            if (user == null)
                return null;

            // NB: Passord lagres i klartekst (kun for demo/prosjekt)
            if (user.Password != password)
                return null;

            return user;
        }
    }
}
