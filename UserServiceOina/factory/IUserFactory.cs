using UserServiceOina.entity;
using UserServiceOina.model;

namespace UserServiceOina.factory;

public interface IUserFactory
{
    User CreateFromRegistrationParams(UserRegistrationParams userRegistrationParams);
}