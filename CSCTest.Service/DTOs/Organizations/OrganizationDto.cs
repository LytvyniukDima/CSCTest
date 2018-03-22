namespace CSCTest.Service.DTOs.Organizations
{
    public class OrganizationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public int OwnerId { get; set; }
        public bool HasChildren { get; set; }
    }
}