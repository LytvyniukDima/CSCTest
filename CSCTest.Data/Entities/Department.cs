namespace CSCTest.Data.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int FamilyOfferingId { get; set; }
        public FamilyOffering FamilyOffering { get; set; }
    }
}