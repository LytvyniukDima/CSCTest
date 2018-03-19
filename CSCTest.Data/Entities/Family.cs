using System.Collections.Generic;
using CSCTest.Data.Abstract;

namespace CSCTest.Data.Entities
{
    public class Family : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int BusinessId { get; set; }
        public Business Business { get; set; }

        public ICollection<BusinessFamily> BusinessFamilies { get; set; }
    }
}