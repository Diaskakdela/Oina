namespace ToysService.toy.entity;

public class Toy
{
    public Toy(string name, string description, string ageRange, Guid categoryId, decimal price, string imageUrl)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        AgeRange = ageRange;
        CategoryId = categoryId;
        Price = price;
        ImageUrl = imageUrl;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string AgeRange { get; set; }

    public Guid CategoryId { get; set; }

    public decimal Price { get; set; }
    
    public string ImageUrl { get; set; }
}