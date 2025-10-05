using ContactManager.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Contact> Contact { get; set; }
    }
}