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
        /// I en produksjonsløsning bør dette lagres som hash, ikke klartekst.
        /// </summary>
        public string Password { get; set; }
    
        /// <summary>
        /// Brukerens rolle i systemet.
        /// </summary>
        public RoleType Role { get; protected set; }
    }
}
