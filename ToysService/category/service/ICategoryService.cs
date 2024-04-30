using ToysService.category.entity;

namespace ToysService.category.service;

public interface ICategoryService
{
    Category Create(String categoryName);

    ICollection<Category> FindAll();
}