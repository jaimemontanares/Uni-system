using System;
using System.Linq;
using UniversitySystem.Models;

namespace UniversitySystem.Services
{
    /// <summary>
    /// Serviceklasse som håndterer registrering og innlogging.
    /// </summary>
    public class AuthorizationService
    {
        private readonly UserService _userService;

        /// <summary>
        /// Oppretter en ny AuthorizationService.
        /// </summary>
        /// <param name="userService">Tjeneste for brukerhåndtering.</param>
        public AuthorizationService(UserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        /// <summary>
        /// Logger inn en bruker dersom brukernavn og passord stemmer.
        /// Returnerer brukeren ved vellykket innlogging, ellers null.
        /// </summary>
        /// <param name="username">Brukernavn.</param>
        /// <param name="password">Passord.</param>
        /// <returns>Innlogget bruker eller null.</returns>
        public User? Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            return _userService
                .GetAllUsers()
                .FirstOrDefault(user =>
                    user.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                    user.Password == password);
        }

        /// <summary>
        /// Registrerer en ny student dersom input er gyldig og brukernavn/e-post ikke er i bruk.
        /// </summary>
        /// <param name="userId">Intern bruker-ID.</param>
        /// <param name="studentId">Student-ID.</param>
        /// <param name="name">Navn på studenten.</param>
        /// <param name="email">E-postadresse.</param>
        /// <param name="username">Ønsket brukernavn.</param>
        /// <param name="password">Ønsket passord.</param>
        /// <param name="message">Resultatmelding.</param>
        /// <returns>True hvis registrering lyktes, ellers false.</returns>
        public bool RegisterStudent(
            string userId,
            string studentId,
            string name,
            string email,
            string username,
            string password,
            out string message)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                message = "Bruker-ID må fylles ut.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(studentId))
            {
                message = "Student-ID må fylles ut.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                message = "Navn må fylles ut.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                message = "E-post må fylles ut.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                message = "Brukernavn må fylles ut.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                message = "Passord må fylles ut.";
                return false;
            }

            if (_userService.FindById(userId) != null)
            {
                message = "Bruker-ID finnes allerede.";
                return false;
            }

            if (_userService.GetAllUsers().Any(user =>
                user.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                message = "Brukernavnet er allerede i bruk.";
                return false;
            }

            if (_userService.GetAllUsers().Any(user =>
                user.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                message = "E-postadressen er allerede i bruk.";
                return false;
            }

            var student = new Student(
                id: userId,
                studentId: studentId,
                name: name,
                email: email,
                username: username,
                password: password);

            bool added = _userService.AddUser(student);

            if (!added)
            {
                message = "Registrering mislyktes.";
                return false;
            }

            message = "Studenten ble registrert.";
            return true;
        }

        /// <summary>
        /// Registrerer en ny utvekslingsstudent dersom input er gyldig.
        /// </summary>
        /// <param name="userId">Intern bruker-ID.</param>
        /// <param name="studentId">Student-ID.</param>
        /// <param name="name">Navn på studenten.</param>
        /// <param name="email">E-postadresse.</param>
        /// <param name="username">Ønsket brukernavn.</param>
        /// <param name="password">Ønsket passord.</param>
        /// <param name="homeUniversity">Hjemuniversitet.</param>
        /// <param name="country">Land.</param>
        /// <param name="periodFrom">Startdato for oppholdet.</param>
        /// <param name="periodTo">Sluttdato for oppholdet.</param>
        /// <param name="message">Resultatmelding.</param>
        /// <returns>True hvis registrering lyktes, ellers false.</returns>
        public bool RegisterExchangeStudent(
            string userId,
            string studentId,
            string name,
            string email,
            string username,
            string password,
            string homeUniversity,
            string country,
            DateTime periodFrom,
            DateTime periodTo,
            out string message)
        {
            if (periodTo < periodFrom)
            {
                message = "Sluttdato kan ikke være før startdato.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(homeUniversity))
            {
                message = "Hjemuniversitet må fylles ut.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(country))
            {
                message = "Land må fylles ut.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(userId) ||
                string.IsNullOrWhiteSpace(studentId) ||
                string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                message = "Alle obligatoriske felt må fylles ut.";
                return false;
            }

            if (_userService.FindById(userId) != null)
            {
                message = "Bruker-ID finnes allerede.";
                return false;
            }

            if (_userService.GetAllUsers().Any(user =>
                user.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                message = "Brukernavnet er allerede i bruk.";
                return false;
            }

            if (_userService.GetAllUsers().Any(user =>
                user.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                message = "E-postadressen er allerede i bruk.";
                return false;
            }

            var exchangeStudent = new ExchangeStudent(
                id: userId,
                studentId: studentId,
                name: name,
                email: email,
                username: username,
                password: password,
                homeUniversity: homeUniversity,
                country: country,
                periodFrom: periodFrom,
                periodTo: periodTo);

            bool added = _userService.AddUser(exchangeStudent);

            if (!added)
            {
                message = "Registrering mislyktes.";
                return false;
            }

            message = "Utvekslingsstudenten ble registrert.";
            return true;
        }

        /// <summary>
        /// Registrerer en ny faglærer dersom input er gyldig.
        /// </summary>
        public bool RegisterLecturer(
            string userId,
            string employeeId,
            string name,
            string email,
            string username,
            string password,
            string department,
            out string message)
        {
            if (string.IsNullOrWhiteSpace(userId) ||
                string.IsNullOrWhiteSpace(employeeId) ||
                string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(department))
            {
                message = "Alle felt må fylles ut.";
                return false;
            }

            if (_userService.FindById(userId) != null)
            {
                message = "Bruker-ID finnes allerede.";
                return false;
            }

            if (_userService.GetAllUsers().Any(user =>
                user.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                message = "Brukernavnet er allerede i bruk.";
                return false;
            }

            if (_userService.GetAllUsers().Any(user =>
                user.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                message = "E-postadressen er allerede i bruk.";
                return false;
            }

            var lecturer = new Lecturer(
                id: userId,
                employeeId: employeeId,
                name: name,
                email: email,
                username: username,
                password: password,
                department: department);

            bool added = _userService.AddUser(lecturer);

            if (!added)
            {
                message = "Registrering mislyktes.";
                return false;
            }

            message = "Faglæreren ble registrert.";
            return true;
        }

        /// <summary>
        /// Registrerer en ny bibliotekar dersom input er gyldig.
        /// </summary>
        public bool RegisterLibrarian(
            string userId,
            string employeeId,
            string name,
            string email,
            string username,
            string password,
            string department,
            out string message)
        {
            if (string.IsNullOrWhiteSpace(userId) ||
                string.IsNullOrWhiteSpace(employeeId) ||
                string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(department))
            {
                message = "Alle felt må fylles ut.";
                return false;
            }

            if (_userService.FindById(userId) != null)
            {
                message = "Bruker-ID finnes allerede.";
                return false;
            }

            if (_userService.GetAllUsers().Any(user =>
                user.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                message = "Brukernavnet er allerede i bruk.";
                return false;
            }

            if (_userService.GetAllUsers().Any(user =>
                user.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                message = "E-postadressen er allerede i bruk.";
                return false;
            }

            var librarian = new Librarian(
                id: userId,
                employeeId: employeeId,
                name: name,
                email: email,
                username: username,
                password: password,
                department: department);

            bool added = _userService.AddUser(librarian);

            if (!added)
            {
                message = "Registrering mislyktes.";
                return false;
            }

            message = "Bibliotekaren ble registrert.";
            return true;
        }
    }
}
