using Demo.GRPC.Endpoint.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.GRPC.Endpoint.Services;

public class Db(DbContextOptions<Db> options) : DbContext(options)
{
    public DbSet<Movement> Movements { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movement>().ToTable("movements");
    }
}