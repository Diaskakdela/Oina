using UserServiceOina.entity;
using UserServiceOina.model;

namespace UserServiceOina.repository;

public interface IUserRepository
{
    User? FindUserByEmail(string email);

    User RegisterUserWithRole(User user, RoleEnum role);
}