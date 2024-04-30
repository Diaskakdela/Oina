using ToysService.toy.entity;
using ToysService.toy.model;

namespace ToysService.toy.service;

public interface IToyService
{
    ICollection<Toy> FindAll();

    Toy? FindById(Guid toyId);

    Toy Create(ToyCreationParams creationParams);

    Toy UpdateById(Guid toyId, ToyUpdateParams updateParams);

    void DeleteById(Guid toyId);
}