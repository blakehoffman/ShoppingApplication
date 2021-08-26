using System;

namespace Domain.Models.Categories
{
    public class Category
    {
        public Category(Guid id, string name, Guid? parentId)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException("id cannot be empty");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name cannot be null or empty");
            }

            if (name.Length < 2 || name.Length > 100)
            {
                throw new ArgumentException("name must be greater than 2 characters and less than 100");
            }

            Id = id;
            Name = name;
            ParentId = parentId;
        }

        public Guid Id { get; }
        public string Name { get; }
        public Guid? ParentId { get; }
    }
}
