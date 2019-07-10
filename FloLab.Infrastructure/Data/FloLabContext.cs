using FloLab.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FloLab.Infrastructure.Data
{
    public class FloLabContext : IdentityDbContext<User>
    {
        public DbSet<User> ApplicationUsers { get; set; }
        public DbSet<Role> ApplicationRoles { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Space> Spaces { get; set; }
        public DbSet<UserDay> UserDays { get; set; }

        public FloLabContext(DbContextOptions<FloLabContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
