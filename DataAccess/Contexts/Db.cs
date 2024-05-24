using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    public class Db : DbContext
    {
        public DbSet<Food> Foods { get; set; }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserFood> UserFoods { get; set; }

        public Db(DbContextOptions options) : base(options) // super in Java
        {
            
        }
    }
}
