using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskMaster.DataAccess.Repository;
using TaskMaster.Models;

namespace TaskMaster.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
            public DbSet<Carpark> Carparks { get; set; }
            public DbSet<Gantry> Gantries { get; set; }
           
            public DbSet<Hardwaretype> Hardwaretypes { get; set; }

            public DbSet<User> Login { get; set; }

            public DbSet<Issue> Issues { get; set; }

    }
}
