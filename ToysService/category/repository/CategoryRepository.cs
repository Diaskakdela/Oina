using ToysService.category.entity;
using ToysService.core;

namespace ToysService.category.repository;

public class CategoryRepository(ApplicationDbContext context) : ICategoryRepository
{
    public Category Create(Category category)
    {
        var categoryEntity = context.Categories.Add(category);
        context.SaveChanges();
        return categoryEntity.Entity;
    }

    public ICollection<Category> FindAll()
    {
        return context.Categories.ToList();
    }
}