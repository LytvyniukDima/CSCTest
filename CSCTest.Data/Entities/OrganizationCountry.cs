using System.Collections.Generic;
using CSCTest.Data.Abstract;

namespace CSCTest.Data.Entities
{
    public class OrganizationCountry : IEntity
    {
        public int Id { get; set; }

        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<CountryBusiness> CountryBusinesses { get; set; }
    }
}