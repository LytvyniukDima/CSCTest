namespace CSCTest.Api.Models.Families
{
    public class FamilyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BusinessId { get; set; }
        public bool HasChildren { get; set; }
    }
}