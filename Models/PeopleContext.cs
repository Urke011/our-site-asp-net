using Microsoft.EntityFrameworkCore;

namespace our_site_asp_net.Models
{
    public class PeopleContext : DbContext
    {
      
        public PeopleContext(DbContextOptions<PeopleContext> options): base(options)
        {

        }
        public DbSet<EmployeProfile> people { get; set; }


    }

}
