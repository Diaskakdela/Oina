namespace UserServiceOina.model.request;

public class RegistrationRequest(string email, string phone, string password, string firstName, string lastName)
{
    public string Email { get; set; } = email;
    public string Phone { get; set; } = phone;
    public string Password { get; set; } = password;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
}