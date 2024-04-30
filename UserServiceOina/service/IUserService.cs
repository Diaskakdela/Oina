using UserServiceOina.model;

namespace UserServiceOina.service;

public interface IUserService
{
    string RegisterSimpleUser(UserRegistrationParams userRegistrationParams);
    string RegisterAdmin(UserRegistrationParams userRegistrationParams);
}