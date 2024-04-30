using ToysService.category.entity;

namespace ToysService.category.model;

public class CategoryMapper
{
    public CategoryDto MapToDto(Category category)
    {
        return new CategoryDto(category.Id, category.Name);
    }
}