namespace CSCTest.Api.Models.Organizations
{
    public class OrganizationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public int OwnerId { get; set; }
        public bool HasChildren { get; set; }
    }
}