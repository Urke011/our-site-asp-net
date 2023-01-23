﻿using Microsoft.EntityFrameworkCore;

namespace our_site_asp_net.Models
{
    public class PeopleContext : DbContext
    {
      
        public PeopleContext(DbContextOptions<PeopleContext> options): base(options)
        {

        }
        public DbSet<EmployeProfile> people { get; set; }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Jobs> Jobs { get; set; }
    }

}
