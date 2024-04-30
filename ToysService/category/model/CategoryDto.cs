namespace ToysService.category.model;

public class CategoryDto(Guid id, string categoryName)
{
    private Guid Id { get; set; } = id;
    private String CategoryName { get; set; } = categoryName;
}