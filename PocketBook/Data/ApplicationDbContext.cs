using Microsoft.EntityFrameworkCore;
using PocketBook.Model;

namespace PocketBook.Data
{
    public class ApplicationDbContext:DbContext
    {
        // the dbset property will tell efcore that we have a table that need to be created if it doesn't exist
        public virtual DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
