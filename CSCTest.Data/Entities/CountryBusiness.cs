using System.Collections.Generic;

namespace CSCTest.Data.Entities
{
    public class CountryBusiness
    {
        public int Id { get; set; }
        public int OrganizationCountryId { get; set; }
        public OrganizationCountry OrganizationCountry { get; set; }

        public int BusinessId { get; set; }
        public Business Business { get; set; }

        public ICollection<BusinessFamily> BusinessFamilies { get; set; }
    }
}