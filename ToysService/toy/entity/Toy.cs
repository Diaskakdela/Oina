namespace ToysService.toy.entity;

public class Toy(string name, string description, string ageRange, Guid categoryId, decimal price, string imageUrl)
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = name;

    public string Description { get; set; } = description;

    public string AgeRange { get; set; } = ageRange;

    public Guid CategoryId { get; set; } = categoryId;

    public decimal Price { get; set; } = price;

    public string ImageUrl { get; set; } = imageUrl;
}