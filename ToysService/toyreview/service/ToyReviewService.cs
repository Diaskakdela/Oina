using ToysService.toyreview.entity;
using ToysService.toyreview.factory;
using ToysService.toyreview.model;
using ToysService.toyreview.repository;

namespace ToysService.toyreview.service;

public class ToyReviewService(IToyReviewRepository toyReviewRepository, ToyReviewFactory toyReviewFactory)
    : IToyReviewService
{
    public ICollection<ToyReview> FindAllByToyId(Guid toyId)
    {
        return toyReviewRepository.FindAllByToyId(toyId);
    }

    public ToyReview Create(ToyReviewCreationParams creationParams)
    {
        var createdToyReview = toyReviewFactory.Create(creationParams);
        return toyReviewRepository.Create(createdToyReview);
    }
}