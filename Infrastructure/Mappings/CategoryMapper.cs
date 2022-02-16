using Domain.Models.Categories;
using Infrastructure.Records;

namespace Infrastructure.Mappings
{
    public static class CategoryMapper
    {
        public static Category MapToCategory(CategoryRecord categoryRecord)
        {
            if (categoryRecord == null)
            {
                return null;
            }

            return new Category(categoryRecord.Id, categoryRecord.Name, categoryRecord.ParentId);
        }

        public static CategoryRecord MapToCategoryRecord(Category category)
        {
            if (category == null)
            {
                return null;
            }

            return new CategoryRecord
            {
                Id = category.Id,
                Name = category.Name,
                ParentId = category.ParentId
            };
        }
    }
}
