using System;

namespace UniversitySystem.Models
{
    public class ExchangeStudent : Student
    {
        public string HomeUniversity { get; set; }
        public string Country { get; set; }
        public DateTime ExchangePeriodFrom { get; set; }
        public DateTime ExchangePeriodTo { get; set; }

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
