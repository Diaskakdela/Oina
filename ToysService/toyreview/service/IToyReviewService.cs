using ToysService.toyreview.entity;
using ToysService.toyreview.model;

namespace ToysService.toyreview.service;

public interface IToyReviewService
{
    ICollection<ToyReview> FindAllByToyId(Guid toyId);

    ToyReview Create(ToyReviewCreationParams creationParams);
}