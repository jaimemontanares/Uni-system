namespace UniversitySystem.Models
{
    /// <summary>
    /// Representerer en generell bruker i universitetssystemet.
    /// Denne klassen fungerer som basisklasse for studenter og ansatte.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Intern bruker-ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Brukerens fulle navn.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Brukerens e-postadresse.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Brukerens innloggingsnavn.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Brukerens passord.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Rollen som bestemmer hvilken funksjonalitet brukeren har tilgang til.
        /// </summary>
        public RoleType Role { get; set; }

        /// <summary>
        /// Oppretter en ny bruker.
        /// </summary>
        public User(
            string id,
            string name,
            string email,
            string username,
            string password,
            RoleType role)
        {
            Id = id;
            Name = name;
            Email = email;
            Username = username;
            Password = password;
            Role = role;
        }
    }
}
