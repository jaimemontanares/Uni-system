namespace UniversitySystem.Models;

namespace UniversitySystem.Services
{
    /// <summary>
    /// Serviceklasse som håndterer all logikk relatert til brukere.
    /// Ansvar:
    /// - Registrering av brukere
    /// - Oppslag (ID, brukernavn, e-post)
    /// - Validering (f.eks. unike brukernavn)
    /// </summary>
    public class UserService
    {
        // Intern liste over alle brukere i systemet
        private readonly List<User> _users = new();

        /// <summary>
        /// Legger til en ny bruker i systemet.
        /// Returnerer false hvis brukernavn allerede finnes.
        /// </summary>
        public bool AddUser(User user)
        {
            // Beskytter mot null-referanse
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            // Hindrer duplikate brukernavn
            if (UsernameExists(user.Username))
                return false;

            _users.Add(user);
            return true;
        }

        /// <summary>
        /// Returnerer alle brukere (read-only kopi for å beskytte intern liste).
        /// </summary>
        public List<User> GetAllUsers()
        {
            return new List<User>(_users);
        }

        /// <summary>
        /// Finner bruker basert på unik ID.
        /// Returnerer null hvis ikke funnet.
        /// </summary>
        public User? FindById(string id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        /// <summary>
        /// Finner bruker basert på brukernavn (case-insensitive).
        /// </summary>
        public User? FindByUsername(string username)
        {
            return _users.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Finner bruker basert på e-post (case-insensitive).
        /// </summary>
        public User? FindByEmail(string email)
        {
            return _users.FirstOrDefault(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Sjekker om et brukernavn allerede eksisterer i systemet.
        /// </summary>
        public bool UsernameExists(string username)
        {
            return _users.Any(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
    }
}
