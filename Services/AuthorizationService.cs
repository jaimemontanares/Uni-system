namespace UniversitySystem.Services
{
    /// <summary>
    /// Samler applikasjonens tjenester på ett sted.
    /// Klassen skal ikke inneholde tung forretningslogikk.
    /// </summary>
    public class UniversityManager
    {
        public UserService UserService { get; }
        public AuthService AuthService { get; }
        public CourseService CourseService { get; }
        public LibraryService LibraryService { get; }

        public UniversityManager()
        {
            UserService = new UserService();
            AuthService = new AuthService(UserService);
            CourseService = new CourseService(UserService);
            LibraryService = new LibraryService(UserService);
        }
    }
}
