using ToysService.toy.entity;
using ToysService.toy.model;

namespace ToysService.toy.repository;

public interface IToyRepository
{
    ICollection<Toy> FindAll();

    Toy? FindById(Guid toyId);

    Toy Create(Toy toy);
    
    void Update(Toy toy);

    void DeleteById(Guid toyId);
}