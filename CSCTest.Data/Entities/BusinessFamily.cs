using System.Collections.Generic;

namespace CSCTest.Data.Entities
{
    public class BusinessFamily
    {
        public int CountryBusinessId { get; set; }
        public CountryBusiness CountryBusiness { get; set; }

        public int FamilyId { get; set; }
        public Family Family { get; set; }

        public ICollection<FamilyOffering> FamilyOfferings { get; set; }
    }
}