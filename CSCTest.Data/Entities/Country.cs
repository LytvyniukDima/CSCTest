using System.Collections.Generic;

namespace CSCTest.Data.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public ICollection<CountryBusiness> CountryBusinesses { get; set; }
    }
}