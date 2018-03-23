namespace CSCTest.Service.DTOs.Families
{
    public class FamilyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BusinessId { get; set; }
        public bool HasChildren { get; set; }
    }
}