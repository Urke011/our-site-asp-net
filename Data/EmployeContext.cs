using Microsoft.EntityFrameworkCore;
using our_site_asp_net.Models;


namespace our_site_asp_net.Data
{
    public class EmployeContext: DbContext
    {
        DbSet<EmployeProfile> Profiles { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=blogTest;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    } 
}
