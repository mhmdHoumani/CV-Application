using Microsoft.EntityFrameworkCore;

namespace CVApplication.Model
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<Person> Persons { get; set; }
    }
}
