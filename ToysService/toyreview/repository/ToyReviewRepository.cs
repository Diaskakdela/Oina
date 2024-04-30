using ToysService.core;
using ToysService.toyreview.entity;

namespace ToysService.toyreview.repository;

public class ToyReviewRepository(ApplicationDbContext context) : IToyReviewRepository
{
    public ICollection<ToyReview> FindAllByToyId(Guid toyId)
    {
        return context.ToyReviews.ToList();
    }

    public ToyReview Create(ToyReview toyReview)
    {
        var toyReviewEntity = context.ToyReviews.Add(toyReview);
        context.SaveChanges();
        return toyReviewEntity.Entity;
    }
}