#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExotikaTrial2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ExotikaTrial2.Data
{
    public class ExotikaTrial2Context : IdentityDbContext
    {
        public ExotikaTrial2Context (DbContextOptions<ExotikaTrial2Context> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Tourist> Tourists { get; set; }

        public DbSet<Resort> Resorts { get; set; }

        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<HandicraftDealer> HandicraftDealers { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public DbSet<ResortBookings> ResortBookings { get; set; }
        public DbSet<Requirement> Requirements { get; set; }

        public DbSet<Package> Packages { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
    }
}
