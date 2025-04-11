using BookClub2._0.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub2._0
{
    public class BookClubDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<BookClub> BookClubs { get; set; }

        public BookClubDbContext(
            DbContextOptions<BookClubDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the many-to-many relationship between BookClub and User for Members
            modelBuilder.Entity<BookClub>()
                .HasMany(bc => bc.Members)
                .WithMany(u => u.MemberOfBookClubs)
                .UsingEntity<Dictionary<string, object>>(
                    "BookClubUsers",
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Restrict), // Prevent cascading delete
                    j => j.HasOne<BookClub>().WithMany().HasForeignKey("BookClubId").OnDelete(DeleteBehavior.Cascade)); // Allow cascading delete

            // Configure the one-to-many relationship for Owner
            modelBuilder.Entity<BookClub>()
                .HasOne(bc => bc.Owner)
                .WithMany(u => u.OwnedBookClubs)
                .HasForeignKey(bc => bc.OwnerId)
                .OnDelete(DeleteBehavior.Cascade); // Allow cascading delete for Owner
        }
    }
}
