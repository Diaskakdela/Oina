using UserServiceOina.entity;

namespace UserServiceOina.model;

public class UserDetailsMessage(User user)
{
    public Guid Id { get; set; } = user.Id;
    public string Email { get; set; } = user.Email;
    public string FirstName { get; set; } = user.FirstName;
    public string LastName { get; set; } = user.LastName;
}