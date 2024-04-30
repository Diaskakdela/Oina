using UserServiceOina.model;

namespace UserServiceOina.service;

public interface IJwtService
{
    String GenerateToken(UserDetails userDetails);
}