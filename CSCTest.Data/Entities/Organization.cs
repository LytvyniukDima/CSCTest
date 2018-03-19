using System.Collections.Generic;
using CSCTest.Data.Abstract;

namespace CSCTest.Data.Entities
{
    public class Organization : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public OrganizationType Type { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<OrganizationCountry> OrganizationCountries { get; set; }
    }
}