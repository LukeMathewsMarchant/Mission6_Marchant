using Microsoft.EntityFrameworkCore;

namespace Mission6.Models;

public class MoviesContext : DbContext
{
    public MoviesContext(DbContextOptions<MoviesContext> options) : base (options)
    {
    }

    public DbSet<Movie> Movies { get; set; }
    // Add this line to link the Categories table
    public DbSet<Category> Categories { get; set; } 
}