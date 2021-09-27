using System;

namespace Infrastructure.Records
{
    public class ProductRecord
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public int Price { get; set; }
    }
}
