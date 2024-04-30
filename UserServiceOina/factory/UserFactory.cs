using UserServiceOina.entity;
using UserServiceOina.model;

namespace UserServiceOina.factory;

public class UserFactory : IUserFactory
{
    public User CreateFromRegistrationParams(UserRegistrationParams userRegistrationParams)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userRegistrationParams.Password);
        return new User(
            userRegistrationParams.Email,
            userRegistrationParams.Phone,
            hashedPassword,
            userRegistrationParams.FirstName,
            userRegistrationParams.LastName,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }
}