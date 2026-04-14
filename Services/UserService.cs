using System;
using System.Collections.Generic;
using System.Linq;
using UniversitySystem.Models;

namespace UniversitySystem.Services
{
    /// <summary>
    /// Serviceklasse som håndterer all logikk relatert til brukere.
    /// Ansvar:
    /// - Registrering av brukere
    /// - Oppslag på ID, brukernavn og e-post
    /// - Validering av unike verdier
    /// </summary>
    public class UserService
    {
        // Intern liste over alle brukere i systemet.
        private readonly List<User> _users = new();

        /// <summary>
        /// Legger til en ny bruker i systemet.
        /// Returnerer false dersom brukeren er ugyldig eller allerede finnes.
        /// </summary>
        /// <param name="user">Brukeren som skal legges til.</param>
        /// <returns>True hvis brukeren ble lagt til, ellers false.</returns>
        public bool AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(user.Id) ||
                string.IsNullOrWhiteSpace(user.Name) ||
                string.IsNullOrWhiteSpace(user.Email) ||
                string.IsNullOrWhiteSpace(user.Username) ||
                string.IsNullOrWhiteSpace(user.Password))
            {
                return false;
            }

            if (IdExists(user.Id))
            {
                return false;
            }

            if (UsernameExists(user.Username))
            {
                return false;
            }

            if (EmailExists(user.Email))
            {
                return false;
            }

            _users.Add(user);
            return true;
        }

        /// <summary>
        /// Returnerer alle brukere som en kopi av internlisten.
        /// </summary>
        /// <returns>Liste med alle brukere.</returns>
        public List<User> GetAllUsers()
        {
            return new List<User>(_users);
        }

        /// <summary>
        /// Finner en bruker basert på intern bruker-ID.
        /// Returnerer null hvis brukeren ikke finnes.
        /// </summary>
        /// <param name="userId">Intern bruker-ID.</param>
        /// <returns>Bruker eller null.</returns>
        public User? FindById(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return null;
            }

            return _users.FirstOrDefault(user =>
                user.Id.Equals(userId, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Finner en bruker basert på brukernavn.
        /// Returnerer null hvis brukeren ikke finnes.
        /// </summary>
        /// <param name="username">Brukernavn.</param>
        /// <returns>Bruker eller null.</returns>
        public User? FindByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return null;
            }

            return _users.FirstOrDefault(user =>
                user.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Finner en bruker basert på e-postadresse.
        /// Returnerer null hvis brukeren ikke finnes.
        /// </summary>
        /// <param name="email">E-postadresse.</param>
        /// <returns>Bruker eller null.</returns>
        public User? FindByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            return _users.FirstOrDefault(user =>
                user.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Sjekker om en intern bruker-ID allerede finnes i systemet.
        /// </summary>
        /// <param name="userId">Intern bruker-ID.</param>
        /// <returns>True hvis ID-en finnes, ellers false.</returns>
        public bool IdExists(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return false;
            }

            return _users.Any(user =>
                user.Id.Equals(userId, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Sjekker om et brukernavn allerede finnes i systemet.
        /// </summary>
        /// <param name="username">Brukernavn.</param>
        /// <returns>True hvis brukernavnet finnes, ellers false.</returns>
        public bool UsernameExists(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return false;
            }

            return _users.Any(user =>
                user.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Sjekker om en e-postadresse allerede finnes i systemet.
        /// </summary>
        /// <param name="email">E-postadresse.</param>
        /// <returns>True hvis e-posten finnes, ellers false.</returns>
        public bool EmailExists(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            return _users.Any(user =>
                user.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Returnerer alle brukere med en bestemt rolle.
        /// </summary>
        /// <param name="role">Rollen det skal filtreres på.</param>
        /// <returns>Liste med brukere som har rollen.</returns>
        public List<User> GetUsersByRole(RoleType role)
        {
            return _users
                .Where(user => user.Role == role)
                .ToList();
        }
    }
}
