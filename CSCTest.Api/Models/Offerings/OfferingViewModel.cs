namespace CSCTest.Api.Models.Offerings
{
    public class OfferingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FamilyId { get; set; }
        public bool HasChildren { get; set; }
    }
}