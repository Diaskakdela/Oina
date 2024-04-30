namespace ToysService.toyreview.model;

public class ToyReviewCreationParams(string review, Guid toyId)
{
    public string Review { get; set; } = review;
    public Guid ToyId { get; set; } = toyId;
}