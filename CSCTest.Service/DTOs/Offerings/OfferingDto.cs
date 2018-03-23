namespace CSCTest.Service.DTOs.Offerings
{
    public class OfferingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FamilyId { get; set; }
        public bool HasChildren { get; set; }
    }
}