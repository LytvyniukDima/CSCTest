using System.Collections.Generic;

namespace CSCTest.Data.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }

        public ICollection<OrganizationCountry> OrganizationCountries { get; set; }
    }
}