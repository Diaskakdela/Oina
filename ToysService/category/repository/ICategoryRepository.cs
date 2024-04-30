using ToysService.category.entity;

namespace ToysService.category.repository;

public interface ICategoryRepository
{
    Category Create(Category category);

    ICollection<Category> FindAll();
}