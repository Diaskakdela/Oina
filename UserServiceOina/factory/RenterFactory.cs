using UserServiceOina.entity;
using UserServiceOina.model;

namespace UserServiceOina.factory;

public class RenterFactory : IRenterFactory
{
    public Renter Create(RenterCreationParams creationParams)
    {
        return new Renter(Guid.NewGuid(), creationParams.UserId, 0);
    }
}