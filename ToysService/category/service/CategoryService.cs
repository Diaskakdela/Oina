using ToysService.category.entity;
using ToysService.category.repository;

namespace ToysService.category.service;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public Category Create(string categoryName)
    {
        return categoryRepository.Create(new Category(categoryName));
    }

    public ICollection<Category> FindAll()
    {
        return categoryRepository.FindAll();
    }
}