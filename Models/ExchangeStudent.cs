using System;

namespace UniversitySystem.Models
{
    /// <summary>
    /// Representerer en utvekslingsstudent.
    /// </summary>
    public class ExchangeStudent : Student
    {
        /// <summary>
        /// Universitetet studenten kommer fra.
        /// </summary>
        public string HomeUniversity { get; set; }

        /// <summary>
        /// Landet studenten kommer fra.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Startdato for utvekslingsperioden.
        /// </summary>
        public DateTime PeriodFrom { get; set; }

        /// <summary>
        /// Sluttdato for utvekslingsperioden.
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
            Role = RoleType.ExchangeStudent;
            HomeUniversity = homeUniversity;
            Country = country;
            PeriodFrom = periodFrom;
            PeriodTo = periodTo;
        }
    }
}
