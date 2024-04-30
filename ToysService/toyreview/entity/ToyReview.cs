namespace ToysService.toyreview.entity;

public class ToyReview
{
    public ToyReview(string review, Guid toyId)
    {
        Id = Guid.NewGuid();
        Review = review;
        ToyId = toyId;
    }

    public Guid Id { get; set; }
    public string Review { get; set; }
    public Guid ToyId { get; set; }
}