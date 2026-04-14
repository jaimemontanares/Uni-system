using System;

namespace UniversitySystem.Models
{
    /// <summary>
    /// Representerer en utvekslingsstudent.
    /// Arver fra Student og utvider med informasjon om hjemmeuniversitet og oppholdsperiode.
    /// </summary>
    public class ExchangeStudent : Student
    {
        /// <summary>
        /// Navn på studentens hjemmeuniversitet.
        /// </summary>
        public string HomeUniversity { get; set; }

        /// <summary>
        /// Landet studenten kommer fra.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Startdato for utvekslingsoppholdet.
        /// </summary>
        public DateTime PeriodFrom { get; set; }

        /// <summary>
        /// Sluttdato for utvekslingsoppholdet.
        /// </summary>
        public DateTime PeriodTo { get; set; }

        /// <summary>
        /// Oppretter en ny utvekslingsstudent.
        /// </summary>
        public ExchangeStudent(
            string id,
            string studentId,
            string name,
            string email,
            string username,
            string password,
            string homeUniversity,
            string country,
            DateTime periodFrom,
            DateTime periodTo)
            : base(id, studentId, name, email, username, password)
        {
            HomeUniversity = homeUniversity;
            Country = country;
            PeriodFrom = periodFrom;
            PeriodTo = periodTo;
            Role = RoleType.ExchangeStudent;
        }
    }
}
