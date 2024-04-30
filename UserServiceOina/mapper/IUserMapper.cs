using UserServiceOina.model;
using UserServiceOina.model.request;

namespace UserServiceOina.mapper;

public interface IUserMapper
{
    UserRegistrationParams MapToRegistrationParams(RegistrationRequest request);
}