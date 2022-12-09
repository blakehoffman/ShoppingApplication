using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public int ClusterId { get; set; }
        public HierarchyId Hid { get; set; }
        public string Name { get; set; }
    }
}