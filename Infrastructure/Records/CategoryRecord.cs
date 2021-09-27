using System;

namespace Infrastructure.Records
{
    public class CategoryRecord
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
    }
}
