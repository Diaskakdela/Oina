namespace UserServiceOina.model;

public class RenterCreationParams(Guid userId)
{
    public Guid UserId { get; set; } = userId;
}