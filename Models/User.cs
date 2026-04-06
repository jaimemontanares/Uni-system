namespace UniversitySystem.Models
{
    /// <summary>
    /// Abstrakt baseklasse for alle brukere i systemet.
    /// Inneholder felles identitet, innloggingsinformasjon og rolle.
    /// </summary>
    public abstract class User
    {
        /// <summary>
        /// Intern unik ID brukt i systemet.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Fullt navn på brukeren.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// E-postadresse til brukeren.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Brukernavn brukt ved innlogging.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Passord brukt ved innlogging.
        /// I en ekte løsning bør dette hashes.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Rollen avgjør hvilken meny og funksjonalitet brukeren får tilgang til.
        /// </summary>
        public RoleType Role { get; protected set; }

        /// <summary>
        /// Oppretter en ny bruker med felles basisinformasjon.
        /// </summary>
        protected User(
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
