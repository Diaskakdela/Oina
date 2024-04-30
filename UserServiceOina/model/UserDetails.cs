using UserServiceOina.entity;

namespace UserServiceOina.model;

public class UserDetails(Guid renterId, string login, ICollection<string> rolesList)
{
    public Guid RenterId { get; } = renterId;
    public string Login { get; } = login;
    public ICollection<string> RolesList { get; } = rolesList;

    public static UserDetails Create(User user, Guid renterId)
    {
        if (user.UserRoles == null)
        {
            throw new InvalidOperationException("User roles are not loaded.");
        }

        ICollection<string> roles = user.UserRoles
            .Select(ur => ur.Role.ToString().ToUpper())
            .ToList();

        return new UserDetails(renterId, user.Email, roles);
    }
}