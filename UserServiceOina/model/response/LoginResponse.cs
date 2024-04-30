namespace UserServiceOina.model.response;

public class LoginResponse(string token)
{
    public string Token { get; } = token;
}