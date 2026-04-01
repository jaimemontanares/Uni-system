namespace UniversitySystem.Models
{
    /// <summary>
    /// Abstrakt baseklasse for alle brukertyper i systemet.
    /// Inneholder felles egenskaper som navn og e-postadresse.
    /// </summary>
    public abstract class User
    {
        public string Id { get; set; } //ny --> trenger bedre kommentar
        
        // Navn på brukeren.
        public string Name { get; set; }
        
        // E-post brukes som unik identifikator i systemet.
        public string Email { get; set; }

        public string Username { get; set; } //ny --> trenger bedre kommentar
        
        public string Password { get; set; } //ny --> trenger bedre kommentar
        
        public RoleType Role { get; protected set; } //ny --> trenger bedre kommentar

        /// <summary>
        /// Må forbedre kommentar!
        /// </summary>
        /// <param name="name">Brukerens navn.</param>
        /// <param name="email">Brukerens e-postadresse.</param>
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
        
        /// <summary>
        /// Returnerer en lesbar tekstrepresentasjon av brukeren. 
        /// Må dette forbedres 01.04.26?
        /// </summary>
        public override string ToString()
        {
            return $"{Name} ({Email})";
        }
    }
}
