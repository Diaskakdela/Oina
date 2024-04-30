namespace UserServiceOina.entity;

public class User
{
    public User(string email, string phone, string passwordHash, string firstName, string lastName, DateTime createdAt,
        DateTime updatedAt)
    {
        Id = Guid.NewGuid();
        Email = email;
        Phone = phone;
        PasswordHash = passwordHash;
        FirstName = firstName;
        LastName = lastName;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string PasswordHash { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; }
}