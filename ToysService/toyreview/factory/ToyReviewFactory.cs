using ToysService.toyreview.entity;
using ToysService.toyreview.model;

namespace ToysService.toyreview.factory;

public class ToyReviewFactory
{
    public ToyReview Create(ToyReviewCreationParams creationParams)
    {
        return new ToyReview(creationParams.Review, creationParams.ToyId);
    }
}