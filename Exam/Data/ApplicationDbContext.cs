using Microsoft.EntityFrameworkCore;
public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): DbContext(options)
{
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Post> Posts => Set<Post>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Post>().HasMany(p => p.Tags).WithMany(t => t.Posts);
        modelBuilder.Entity<Tag>().HasMany(p => p.Posts).WithMany(t => t.Tags);
    }
}
