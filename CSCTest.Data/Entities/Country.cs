using System;
using CSCTest.Data.Abstract;

namespace CSCTest.Data.Entities
{
    public class Country : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
    }
}