using Microsoft.EntityFrameworkCore;

namespace TetHolidayAPI;

public class TetHolidayDbContext(DbContextOptions<TetHolidayDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}