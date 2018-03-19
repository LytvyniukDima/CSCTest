using System.Collections.Generic;
using CSCTest.Data.Abstract;

namespace CSCTest.Data.Entities
{
    public class Offering : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int FamilyId { get; set; }
        public Family Family { get; set; }

        public ICollection<FamilyOffering> FamilyOfferings { get; set; }
    }
}