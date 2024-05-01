using Microsoft.EntityFrameworkCore;
using ToysService.core;
using ToysService.toy.entity;

namespace ToysService.toy.repository;

public class ToyRepository(ApplicationDbContext applicationDbContext) : IToyRepository
{
    public ICollection<Toy> FindAll()
    {
        return applicationDbContext.Toys.ToList();
    }

    public Toy? FindById(Guid toyId)
    {
        return applicationDbContext.Toys.FirstOrDefault(toy => toy.Id == toyId);
    }

    public Toy Create(Toy toy)
    {
        var entity = applicationDbContext.Toys.Add(toy);
        applicationDbContext.SaveChanges();

        return entity.Entity;
    }

    public void Update(Toy toy)
    {
        applicationDbContext.Entry(toy).State = EntityState.Modified;
        applicationDbContext.SaveChanges();
    }

    public void DeleteById(Guid toyId)
    {
        var toy = applicationDbContext.Toys.FirstOrDefault(toy => toy.Id == toyId);

        if (toy == null)
        {
            throw new KeyNotFoundException($"No toy found with ID {toyId}.");
        }

        applicationDbContext.Toys.Remove(toy);
        applicationDbContext.SaveChanges();
    }
}