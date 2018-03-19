using System.Collections.Generic;
using CSCTest.Data.Abstract;

namespace CSCTest.Data.Entities
{
    public class FamilyOffering : IEntity
    {
        public int Id { get; set; }
        
        public int BussinessFamilyId { get; set; }
        public BusinessFamily BusinessFamily { get; set; }

        public int OfferingId { get; set; }
        public Offering Offering { get; set; }

        public ICollection<Department> Departments { get; set; }
    }
}