namespace ToysService.toy.model.request;

public class ToyCreationRequest
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string AgeRange { get; set; }

    public Guid CategoryId { get; set; }

    public decimal Price { get; set; }
    
    public string ImageUrl { get; set; }
}