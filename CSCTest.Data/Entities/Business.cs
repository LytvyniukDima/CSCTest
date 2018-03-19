using System.Collections.Generic;
using CSCTest.Data.Abstract;

namespace CSCTest.Data.Entities
{
    public class Business : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CountryBusiness> CountryBusinesses { get; set; }
    }
}