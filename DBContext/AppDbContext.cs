using instantBid.Model;
using instantBid.Models;
using Microsoft.EntityFrameworkCore;

namespace instantBid.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Auction> Auctions { get; set; }


        //Data seeding 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "Admin" },
                new Role { RoleId = 2, RoleName = "Bidder" },
                new Role { RoleId = 3, RoleName = "Seller" }
            );

            //Seed Admin Data
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    RoleId = 1,
                    UserId = 1,
                    Name = "Admin",
                    Email = "BidAdmin@gmail.com",
                    Password = "BidManage",
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 10, 10)
                }
            );
        }
    }
}
