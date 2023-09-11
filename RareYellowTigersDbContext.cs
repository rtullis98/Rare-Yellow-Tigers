using Microsoft.EntityFrameworkCore;
namespace Rare_Yellow_Tigers.Models;

public class RareYellowTigersDbContext : DbContext
{
    public DbSet<RareUsers> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Reaction> Reactions { get; set; }

    public RareYellowTigersDbContext(DbContextOptions<RareYellowTigersDbContext> context) : base(context)
    {
    }

}
