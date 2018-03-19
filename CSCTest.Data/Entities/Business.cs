using System.Collections.Generic;

namespace CSCTest.Data.Entities
{
    public class Business
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CountryBusiness> CountryBusinesses { get; set; }
        public ICollection<Family> Families { get; set; }
    }
}