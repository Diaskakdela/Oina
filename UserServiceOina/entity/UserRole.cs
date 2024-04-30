using UserServiceOina.model;

namespace UserServiceOina.entity;

public class UserRole
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public RoleEnum Role { get; set; }
    
    public virtual User User { get; set; }
}