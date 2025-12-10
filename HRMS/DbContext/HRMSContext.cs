using HRMS.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS.DbContexts
{
    public class HRMSContext : DbContext
    {
        public HRMSContext(DbContextOptions<HRMSContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);// call the base method call the parent

            modelBuilder.Entity<Lookup>().HasData(
                    // Employee Postions (Major Code = 0)
                    new Lookup { Id = 1, MajorCode = 0, MinorCode = 0, Name = "Employee Positions" },
                    new Lookup { Id = 2, MajorCode = 0, MinorCode = 1, Name = "Developer" },
                    new Lookup { Id = 3, MajorCode = 0, MinorCode = 2, Name = "HR" },
                    new Lookup { Id = 4, MajorCode = 0, MinorCode = 3, Name = "Manager" },

                    // Department Types (Major Code = 1)
                    new Lookup { Id = 5, MajorCode = 1, MinorCode = 0, Name = "Department Types" },
                    new Lookup { Id = 6, MajorCode = 1, MinorCode = 1, Name = "Finance" },
                    new Lookup { Id = 7, MajorCode = 1, MinorCode = 2, Name = "Adminstrative" },
                    new Lookup { Id = 8, MajorCode = 1, MinorCode = 3, Name = "Technical" }

                );
            modelBuilder.Entity<User>().HasIndex( x => x.Username ).IsUnique();
            modelBuilder.Entity<Employee>().HasIndex( x => x.UserId).IsUnique();// buz 1 to 1 rel so to make one from employee we need this

            modelBuilder.Entity<User>().HasData(
                new User { Id =1, Username="Admin" , IsAdmin = true , HashedPassword = "$2a$11$oRZUJZIpM0sRP855lUEEZe5JZ4CAL1UNGpFjEj6lSGx.wgbZ/na8W" } // Admin@123
                );

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Lookup> Lookups { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
