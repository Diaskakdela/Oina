using UserServiceOina.events;
using UserServiceOina.model;
using UserServiceOina.service.impl;

namespace UserServiceOina.service;

public interface IUserService
{
    public event UserRegistrationEventHandler UserRegistered;
    string RegisterUser(UserRegistrationParams userRegistrationParams);
    string RegisterAdmin(UserRegistrationParams userRegistrationParams);
}