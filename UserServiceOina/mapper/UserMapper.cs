using UserServiceOina.model;
using UserServiceOina.model.request;

namespace UserServiceOina.mapper;

public class UserMapper : IUserMapper
{
    public UserRegistrationParams MapToRegistrationParams(RegistrationRequest request)
    {
        return new UserRegistrationParams(request.Email,
            request.Phone,
            request.Password,
            request.FirstName,
            request.LastName);
    }
}