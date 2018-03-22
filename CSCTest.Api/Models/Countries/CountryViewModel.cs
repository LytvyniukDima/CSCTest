namespace CSCTest.Api.Models.Countries
{
    public class CountryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int OrganizationId { get; set; }
        public bool HasChildern { get; set; }
    }
}