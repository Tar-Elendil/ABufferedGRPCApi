using Demo.StorageEngine.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.StorageEngine.Services;

public class Db(DbContextOptions<Db> options) : DbContext(options)
{
    public DbSet<Models.Movement> Movements { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movement>().ToTable("movements");
    }
}