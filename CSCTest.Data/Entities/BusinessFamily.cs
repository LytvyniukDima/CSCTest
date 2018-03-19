using System.Collections.Generic;
using CSCTest.Data.Abstract;

namespace CSCTest.Data.Entities
{
    public class BusinessFamily : IEntity
    {
        public int Id { get; set; }

        public int CountryBusinessId { get; set; }
        public CountryBusiness CountryBusiness { get; set; }

        public int FamilyId { get; set; }
        public Family Family { get; set; }

        public ICollection<FamilyOffering> FamilyOfferings { get; set; }
    }
}