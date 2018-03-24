namespace CSCTest.Api.Models.Organizations
{
    public class OrganizationTreeViewModel
    {
        public int OrganizationId { get; set; }
        public string Text { get; set; }
        public bool HasChildren { get; set; }
        public string ParentId { get; set; } = "";
    }
}