using System.Collections.Generic;
using CSCTest.Data.Abstract;

namespace CSCTest.Data.Entities
{
    public class CountryBusiness : IEntity
    {
        public int Id { get; set; }

        public int OrganizationCountryId { get; set; }
        public OrganizationCountry OrganizationCountry { get; set; }

        public int BussinesId { get; set; }
        public Business Business { get; set; }

        public ICollection<BusinessFamily> BusinessFamilies { get; set; }
    }
}