using ToysService.toy.entity;
using ToysService.toy.model;

namespace ToysService.toy.factory;

public class ToyFactory
{
    public Toy Create(ToyCreationParams creationParams)
    {
        return new Toy(creationParams.Name, creationParams.Description, creationParams.AgeRange,
            creationParams.CategoryId, creationParams.Price, creationParams.ImageUrl);
    }
}