using UniversitySystem.Models;

namespace UniversitySystem.Services
{
    /// <summary>
    /// Håndterer registrering og innlogging av brukere.
    /// Holder autentiseringslogikk adskilt fra UI og øvrig domenelogikk.
    /// </summary>
    public class AuthService
    {
        private readonly UserService _userService;

        public AuthService(UserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        /// <summary>
        /// Registrerer en ny student dersom brukernavn og input er gyldig.
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
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                message = "Navn, e-post, brukernavn og passord må fylles ut.";
                return false;
            }

            if (_userService.UsernameExists(username))
            {
                message = "Brukernavnet finnes allerede.";
                return false;
            }

            var student = new Student(id, studentId, name, email, username, password);

            bool added = _userService.AddUser(student);
            if (!added)
            {
                message = "Kunne ikke registrere brukeren.";
                return false;
            }

            message = "Student registrert.";
            return true;
        }

        /// <summary>
        /// Returnerer bruker ved vellykket innlogging, ellers null.
        /// </summary>
        public User? Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            var user = _userService.FindByUsername(username);
            if (user == null)
            {
                return null;
            }

            return user.Password == password ? user : null;
        }
    }
}
