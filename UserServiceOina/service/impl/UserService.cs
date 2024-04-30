using UserServiceOina.entity;
using UserServiceOina.exceptions;
using UserServiceOina.factory;
using UserServiceOina.model;
using UserServiceOina.repository;

namespace UserServiceOina.service.impl;

public class UserService(
    IUserRepository userRepository,
    IUserFactory userFactory,
    IJwtService jwtService,
    IRenterService renterService) : IUserService
{
    private string RegisterUser(UserRegistrationParams userRegistrationParams, RoleEnum role)
    {
        if (userRepository.FindUserByEmail(userRegistrationParams.Email) != null)
        {
            throw new UserRegistrationException("User with this email already exists.");
        }

        var user = userFactory.CreateFromRegistrationParams(userRegistrationParams);
        var renterId = renterService.CreateNewRenter(new RenterCreationParams(user.Id));
        var savedUser = userRepository.RegisterUserWithRole(user, role);
        var token = jwtService.GenerateToken(UserDetails.Create(savedUser, renterId));

        return token;
    }

    public string RegisterSimpleUser(UserRegistrationParams userRegistrationParams)
    {
        return RegisterUser(userRegistrationParams, RoleEnum.User);
    }

    public string RegisterAdmin(UserRegistrationParams userRegistrationParams)
    {
        return RegisterUser(userRegistrationParams, RoleEnum.Admin);
    }
}