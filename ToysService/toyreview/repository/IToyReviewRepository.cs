using ToysService.toyreview.entity;

namespace ToysService.toyreview.repository;

public interface IToyReviewRepository
{
    ICollection<ToyReview> FindAllByToyId(Guid toyId);

    ToyReview Create(ToyReview toyReview);
}