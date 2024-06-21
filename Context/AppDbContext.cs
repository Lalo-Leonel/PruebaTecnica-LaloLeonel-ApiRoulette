using ApiRestRoulette.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiRestRoulette.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) 
        {
            
        }
        public DbSet<User> Users { get; set; }
    }
}
