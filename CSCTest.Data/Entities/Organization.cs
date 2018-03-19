using CSCTest.Data.Abstract;

namespace CSCTest.Data.Entities
{
    public class Organization : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
    }
}