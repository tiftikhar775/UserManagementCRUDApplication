using Microsoft.EntityFrameworkCore;
using UserManagementCRUDApplication.Models.DomainModels;

namespace UserManagementCRUDApplication.Data
{
    public class MVCDbContext : DbContext // inherit db context module from ef core framework
    {
        public MVCDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
