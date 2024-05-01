using Microsoft.EntityFrameworkCore;
using ToysService.category.entity;
using ToysService.toy.entity;
using ToysService.toyreview.entity;

namespace ToysService.core;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Toy> Toys { get; set; }
    public DbSet<ToyReview> ToyReviews { get; set; }
}