namespace UniversitySystem.Services
{
    /// <summary>
    /// Samler systemets sentrale tjenester på ett sted.
    /// Klassen fungerer som et enkelt tilgangspunkt til bruker-,
    /// autentiserings-, kurs- og biblioteklogikk.
    /// </summary>
    public class UniversityManager
    {
        /// <summary>
        /// Håndterer oppslag, registrering og administrasjon av brukere.
        /// </summary>
        public UserService UserService { get; }

        /// <summary>
        /// Håndterer registrering og innlogging.
        /// </summary>
        public AuthorizationService AuthorizationService { get; }

        /// <summary>
        /// Håndterer kurs, påmelding, pensum og karakterer.
        /// </summary>
        public CourseService CourseService { get; }

        /// <summary>
        /// Håndterer bøker, utlån og innlevering.
        /// </summary>
        public LibraryService LibraryService { get; }

        /// <summary>
        /// Oppretter alle nødvendige tjenester for universitetssystemet.
        /// </summary>
        public UniversityManager()
        {
            UserService = new UserService();
            AuthorizationService = new AuthorizationService(UserService);
            CourseService = new CourseService(UserService);
            LibraryService = new LibraryService(UserService);
        }
    }
}
