using ToysService.toy.entity;
using ToysService.toy.exceptions;
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
        var toy = FindById(toyId);
        if (toy == null)
        {
            throw new ToyNotFoundException($"Toy with id={toyId} not found!");
        }

        UpdateExistsParams(toy, updateParams);
        toyRepository.Update(toy);
        return toy;
    }

    public void DeleteById(Guid toyId)
    {
        toyRepository.DeleteById(toyId);
    }

    private void UpdateExistsParams(Toy toyToUpdate, ToyUpdateParams updateParams)
    {
        if (!string.IsNullOrEmpty(updateParams.Name))
            toyToUpdate.Name = updateParams.Name;
        if (!string.IsNullOrEmpty(updateParams.Description))
            toyToUpdate.Description = updateParams.Description;
        if (!string.IsNullOrEmpty(updateParams.AgeRange))
            toyToUpdate.AgeRange = updateParams.AgeRange;
        if (updateParams.CategoryId != Guid.Empty)
            toyToUpdate.CategoryId = updateParams.CategoryId;
        if (updateParams.Price > 0)
            toyToUpdate.Price = updateParams.Price;
        if (!string.IsNullOrEmpty(updateParams.ImageUrl))
            toyToUpdate.ImageUrl = updateParams.ImageUrl;
    }
}