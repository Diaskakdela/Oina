using UserServiceOina.config;
using UserServiceOina.entity;

namespace UserServiceOina.repository.impl;

public class RenterRepository(ApplicationDbContext context) : IRenterRepository
{
    public Renter Save(Renter renter)
    {
        context.Renters.Add(renter);
        context.SaveChanges();
        return renter;
    }

    public Renter? FindByUserId(Guid userId)
    {
        return context.Renters.FirstOrDefault(renter => renter.UserId == userId);
    }
}