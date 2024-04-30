namespace ToysService.category.entity;

public class Category
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Category(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}