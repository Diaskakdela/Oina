using ToysService.toy.entity;
using ToysService.toy.factory;
using ToysService.toy.model;
using ToysService.toy.repository;

namespace ToysService.toy.service;

public class ToyService(IToyRepository toyRepository, ToyFactory toyFactory) : IToyService
{
    public ICollection<Toy> FindAll()
    {
        return toyRepository.FindAll();
    }

    public Toy? FindById(Guid toyId)
    {
        return toyRepository.FindById(toyId);
    }

    public Toy Create(ToyCreationParams creationParams)
    {
        var toy = toyFactory.Create(creationParams);
        return toyRepository.Create(toy);
    }

    public Toy UpdateById(Guid toyId, ToyUpdateParams updateParams)
    {
        return toyRepository.UpdateById(toyId, updateParams);
    }

    public void DeleteById(Guid toyId)
    {
        toyRepository.DeleteById(toyId);
    }
}