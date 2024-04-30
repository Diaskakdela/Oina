using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserServiceOina.entity;

namespace UserServiceOina.config;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Renter> Renters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var roleConverter = new ValueConverter<RoleEnum, string>(
            v => v.ToString().ToUpper(),
            v => (RoleEnum)Enum.Parse(typeof(RoleEnum), v, true));

        modelBuilder.Entity<UserRole>()
            .Property(e => e.Role)
            .HasConversion(roleConverter);

        base.OnModelCreating(modelBuilder);
    }
}