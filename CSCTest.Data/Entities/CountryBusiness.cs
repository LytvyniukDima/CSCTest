using System.Collections.Generic;

namespace CSCTest.Data.Entities
{
    public class CountryBusiness
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }

        public int BusinessId { get; set; }
        public Business Business { get; set; }

        public ICollection<BusinessFamily> BusinessFamilies { get; set; }
    }
}