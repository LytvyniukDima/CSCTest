using CSCTest.Data.Abstract;

namespace CSCTest.Data.Entities
{
    public class Department : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int FamilyOfferingId { get; set; }
        public FamilyOffering FamilyOffering { get; set; }
    }
}