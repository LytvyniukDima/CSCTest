namespace CSCTest.Api.Models.Businesses
{
    public class BusinessViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public bool HasChildren { get; set; }
    }
}