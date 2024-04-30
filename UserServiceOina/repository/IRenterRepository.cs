using UserServiceOina.entity;

namespace UserServiceOina.repository;

public interface IRenterRepository
{
    Renter Save(Renter renter);
    Renter? FindByUserId(Guid userId);
}