using UserServiceOina.entity;
using UserServiceOina.model;

namespace UserServiceOina.factory;

public interface IRenterFactory
{
    Renter Create(RenterCreationParams creationParams);
}