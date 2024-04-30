using System.Security.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserServiceOina.exceptions;
using UserServiceOina.mapper;
using UserServiceOina.model.request;
using UserServiceOina.model.response;
using UserServiceOina.service;

namespace UserServiceOina.controller;

[ApiController]
[Route("")]
public class UserController(IAuthService authService, IUserService userService, IUserMapper userMapper) : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        try
        {
            var token = authService.Authenticate(loginRequest.Email, loginRequest.Password);

            return Ok(new LoginResponse(token));
        }
        catch (AuthenticationException e)
        {
            return Unauthorized(e.Message);
        }
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public IActionResult Register([FromBody] RegistrationRequest registrationRequest)
    {
        try
        {
            var token = userService.RegisterSimpleUser(userMapper.MapToRegistrationParams(registrationRequest));
            return Ok(new { Token = token });
        }
        catch (UserRegistrationException ure)
        {
            return Conflict(ure.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, "An error occurred while registering the user.");
        }
    }

    [HttpPost("admin/register")]
    [Authorize(Roles = "ADMIN")]
    public IActionResult RegisterAdmin([FromBody] RegistrationRequest registrationRequest)
    {
        try
        {
            var token = userService.RegisterAdmin(userMapper.MapToRegistrationParams(registrationRequest));
            return Ok(new { Token = token });
        }
        catch (UserRegistrationException ure)
        {
            return Conflict(ure.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, "An error occurred while registering the user.");
        }
    }
}