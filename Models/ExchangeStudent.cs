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
        /// <param name="name">Studentens navn.</param>
        /// <param name="email">Studentens e-postadresse.</param>
        /// <param name="studentId">Studentens ID.</param>
        /// <param name="homeUniversity">Universitetet studenten kommer fra.</param>
        /// <param name="country">Studentens hjemland.</param>
        public ExchangeStudent(
            string name,
            string email,
            string studentId,
            string homeUniversity,
            string country)
            : base(name, email, studentId)
        {
            HomeUniversity = homeUniversity;
            Country = country;
        }
    }
}
