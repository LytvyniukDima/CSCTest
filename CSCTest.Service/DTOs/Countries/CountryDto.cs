namespace CSCTest.Service.DTOs.Countries
{
    public class CountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int OrganizationId { get; set; }
        public bool HasChildren { get; set; }
    }
}