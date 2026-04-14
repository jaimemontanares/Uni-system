using System;

namespace UniversitySystem.Services
{
    /// <summary>
    /// Samler systemets sentrale tjenester på ett sted.
    /// Denne klassen brukes som et enkelt kontaktpunkt mellom UI og service-laget.
    /// </summary>
    public class UniversityManager
    {
        /// <summary>
        /// Tjeneste for håndtering av brukere.
        /// </summary>
        public UserService UserService { get; }

        /// <summary>
        /// Tjeneste for innlogging og registrering.
        /// </summary>
        public AuthorizationService AuthorizationService { get; }

        /// <summary>
        /// Tjeneste for kurs og kurspåmelding.
        /// </summary>
        public CourseService CourseService { get; }

        /// <summary>
        /// Tjeneste for bibliotek og utlån.
        /// </summary>
        public LibraryService LibraryService { get; }

        /// <summary>
        /// Oppretter en ny instans av UniversityManager og initialiserer alle tjenester
        /// i riktig rekkefølge.
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
