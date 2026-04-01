using System;

namespace UniversitySystem.Models
{
    /// <summary>
    /// Representerer en utvekslingsstudent.
    /// Arver fra Student og inneholder ekstra informasjon om hjemuniversitet og utvekslingsperiode.
    /// </summary>
    public class ExchangeStudent : Student
    {
        // Universitetet studenten kommer fra.
        public string HomeUniversity { get; set; }
        // Landet studenten er registrert fra.
        public string Country { get; set; }
        // Startdato for utvekslingsoppholdet.
        public DateTime ExchangePeriodFrom { get; set; }
        // Sluttdato for utvekslingsoppholdet.
        public DateTime ExchangePeriodTo { get; set; }
        
        /// <summary>
        /// Oppretter en ny utvekslingsstudent med grunnleggende studentinformasjon
        /// samt informasjon om hjemuniversitet og opprinnelsesland.
        /// </summary>
        /// NY Features --> bedre kommentar 01.04.26!
        /// <param name="name">Studentens navn.</param>
        /// <param name="email">Studentens e-postadresse.</param>
        /// <param name="studentId">Studentens ID.</param>
        /// <param name="homeUniversity">Universitetet studenten kommer fra.</param>
        /// <param name="country">Studentens hjemland.</param>
        public ExchangeStudent(
            string id,
            string name,
            string email,
            string username,
            string password,
            string studentId,
            string homeUniversity,
            string country,
            DateTime exchangePeriodFrom
            DateTime exchangePeriodTo)
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
