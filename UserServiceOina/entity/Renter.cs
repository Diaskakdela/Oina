namespace UserServiceOina.entity;

public class Renter(Guid id, Guid userId, int totalToysRented)
{
    public Guid Id { get; set; } = id;
    public Guid UserId { get; set; } = userId;
    public Int32 TotalToysRented { get; set; } = totalToysRented;
}