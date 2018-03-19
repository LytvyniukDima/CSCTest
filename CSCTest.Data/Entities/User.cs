using System.Collections.Generic;
using CSCTest.Data.Abstract;

namespace CSCTest.Data.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }

        public ICollection<Organization> Organizations { get; set; }
    }
}