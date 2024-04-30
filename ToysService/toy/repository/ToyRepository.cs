using ToysService.core;
using ToysService.toy.entity;
using ToysService.toy.model;

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

    public Toy UpdateById(Guid toyId, ToyUpdateParams updateParams)
    {
        var foundToy = applicationDbContext.Toys.FirstOrDefault(toy => toy.Id == toyId);
        if (foundToy == null)
        {
            throw new KeyNotFoundException($"Toy with ID {toyId} was not found.");
        }

        var updatedToy = UpdateOnlyExistsParams(foundToy, updateParams);

        var toyEntity = applicationDbContext.Toys.Add(updatedToy);
        applicationDbContext.SaveChanges();

        return toyEntity.Entity;
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

    private Toy UpdateOnlyExistsParams(Toy toyToUpdate, ToyUpdateParams updateParams)
    {
        if (!string.IsNullOrEmpty(updateParams.Name))
        {
            toyToUpdate.Name = updateParams.Name;
        }

        if (!string.IsNullOrEmpty(updateParams.Description))
        {
            toyToUpdate.Description = updateParams.Description;
        }

        if (!string.IsNullOrEmpty(updateParams.AgeRange))
        {
            toyToUpdate.AgeRange = updateParams.AgeRange;
        }

        if (updateParams.CategoryId != Guid.Empty)
        {
            toyToUpdate.CategoryId = updateParams.CategoryId;
        }

        if (updateParams.Price > 0)
        {
            toyToUpdate.Price = updateParams.Price;
        }

        if (!string.IsNullOrEmpty(updateParams.ImageUrl))
        {
            toyToUpdate.ImageUrl = updateParams.ImageUrl;
        }

        return toyToUpdate;
    }
}