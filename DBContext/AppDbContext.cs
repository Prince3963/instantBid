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
        public DbSet<Items> Items { get; set; }
        public DbSet<BidHistory> BidHistories { get; set; }
        public DbSet<Winner> Winners { get; set; }



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


            modelBuilder.Entity<BidHistory>()
        .HasOne(b => b.User)
        .WithMany(u => u.BidHistories)
        .HasForeignKey(b => b.UserId)
        .OnDelete(DeleteBehavior.Restrict);  //  prevent multiple cascade paths

            // BidHistory → Auction (Cascade OK)
            modelBuilder.Entity<BidHistory>()
                .HasOne(b => b.Auction)
                .WithMany()
                .HasForeignKey(b => b.AuctionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Auction → User (Restrict)
            modelBuilder.Entity<Auction>()
                .HasOne(a => a.User)
                .WithMany(u => u.Auctions)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Items → User (Restrict)
            modelBuilder.Entity<Items>()
                .HasOne(i => i.User)
                .WithMany(u => u.Items)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Auction>()
            .HasOne(a => a.WinnerUser)
            .WithMany()
            .HasForeignKey(a => a.WinnerUserId)
            .OnDelete(DeleteBehavior.Restrict);

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
