using UserServiceOina.entity;
using UserServiceOina.events;
using UserServiceOina.exceptions;
using UserServiceOina.factory;
using UserServiceOina.model;
using UserServiceOina.repository;

namespace UserServiceOina.service.impl;

public class UserService(
    IUserRepository userRepository,
    IUserFactory userFactory,
    IJwtService jwtService,
    IRenterService renterService,
    ILogger<UserService> logger) : IUserService
{
    public event UserRegistrationEventHandler? UserRegistered;

    private string RegisterWithRole(UserRegistrationParams userRegistrationParams, RoleEnum role)
    {
        logger.LogInformation(
            $"Attempting to register new user with email: {userRegistrationParams.Email} and role: {role}");
        if (userRepository.FindUserByEmail(userRegistrationParams.Email) != null)
        {
            logger.LogWarning(
                $"Registration attempt failed: User with email {userRegistrationParams.Email} already exists.");
            throw new UserRegistrationException("User with this email already exists.");
        }

        var user = userFactory.CreateFromRegistrationParams(userRegistrationParams);
        var savedUser = userRepository.RegisterUserWithRole(user, role);
        var renterId = renterService.CreateNewRenter(new RenterCreationParams(savedUser.Id));
        logger.LogInformation($"User registered successfully with ID: {savedUser.Id}");

        var token = jwtService.GenerateToken(JwtUserDetails.Create(savedUser, renterId));

        OnUserRegistered(savedUser);

        logger.LogInformation($"JWT token generated for user ID: {savedUser.Id}");

        return token;
    }

    public string RegisterUser(UserRegistrationParams userRegistrationParams)
    {
        return RegisterWithRole(userRegistrationParams, RoleEnum.User);
    }

    public string RegisterAdmin(UserRegistrationParams userRegistrationParams)
    {
        return RegisterWithRole(userRegistrationParams, RoleEnum.Admin);
    }

    protected virtual void OnUserRegistered(User user)
    {
        UserRegistered?.Invoke(this, new UserRegisteredEventArgs() { User = user });
    }
}

public delegate void UserRegistrationEventHandler(UserService sender, UserRegisteredEventArgs e);