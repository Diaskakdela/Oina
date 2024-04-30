using Microsoft.EntityFrameworkCore;
using UserServiceOina.config;
using UserServiceOina.entity;

namespace UserServiceOina.repository.impl;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public User? FindUserByEmail(string email)
    {
        return context.Users
            .Include(u => u.UserRoles)
            .FirstOrDefault(u => u.Email == email);
    }

    public User RegisterUserWithRole(User user, RoleEnum role)
    {
        using var transaction = context.Database.BeginTransaction();
        try
        {
            context.Users.Add(user);
            context.SaveChanges();

            var userRole = new UserRole
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Role = role
            };
            context.UserRoles.Add(userRole);
            context.SaveChanges();

            transaction.Commit();
            return user;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}