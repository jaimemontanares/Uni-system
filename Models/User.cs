namespace UniversitySystem.Models
{
    /// <summary>
    /// Abstrakt baseklasse for alle brukertyper i systemet.
    /// Inneholder felles egenskaper som navn og e-postadresse.
    /// </summary>
    public abstract class User
    {
        // Navn på brukeren.
        public string Name { get; set; }
        
        // E-post brukes som unik identifikator i systemet.
        public string Email { get; set; }

        /// <summary>
        /// Oppretter en ny bruker med navn og e-post.
        /// </summary>
        /// <param name="name">Brukerens navn.</param>
        /// <param name="email">Brukerens e-postadresse.</param>
        protected User(string name, string email)
        {
            Name = name;
            Email = email;
        }
        
        /// <summary>
        /// Returnerer en lesbar tekstrepresentasjon av brukeren.
        /// </summary>
        public override string ToString()
        {
            return $"{Name} ({Email})";
        }
    }
}
