using UserServiceOina.factory;
using UserServiceOina.model;
using UserServiceOina.repository;

namespace UserServiceOina.service.impl;

public class RenterService(IRenterRepository renterRepository, IRenterFactory renterFactory) : IRenterService
{
    public Guid CreateNewRenter(RenterCreationParams renterCreationParams)
    {
        var renter = renterFactory.Create(renterCreationParams);
        return renterRepository.Save(renter).Id;
    }
}