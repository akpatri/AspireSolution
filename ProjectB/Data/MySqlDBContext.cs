using Microsoft.EntityFrameworkCore;
using ProjectB.Model;

namespace ProjectB.Data
{
    public class MySqlDBContext:DbContext
    {
        public MySqlDBContext(DbContextOptions<MySqlDBContext> options) : base(options)
        {
        }
        public DbSet<CustomerModel> Customers { get; set; } = null;
    }
   
}
