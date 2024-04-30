using UserServiceOina.model;

namespace UserServiceOina.service;

public interface IRenterService
{
    Guid CreateNewRenter(RenterCreationParams renterCreationParams);
}