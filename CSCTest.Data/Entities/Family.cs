using System.Collections.Generic;

namespace CSCTest.Data.Entities
{
    public class Family
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int BusinessId { get; set; }
        public Business Business { get; set; }

        public ICollection<BusinessFamily> BusinessFamilies { get; set; }
    }
}