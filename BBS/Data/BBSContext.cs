using BBS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BBS.Data
{
    public class BBSContext : DbContext
    {
        public BBSContext(DbContextOptions<BBSContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasKey(m => m.Id);
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasKey(m => m.Id);
            modelBuilder.Entity<IdentityUserClaim<string>>().HasKey(m => m.Id);
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(m => m.UserId);
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(m => m.UserId);
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(m => m.UserId);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Node>().ToTable("Node");
            modelBuilder.Entity<Topic>().ToTable("Topic");
            modelBuilder.Entity<Reply>().ToTable("Reply");
            modelBuilder.Entity<NodeRecord>().ToTable("NodeRecord");
            modelBuilder.Entity<TopicRecord>().ToTable("TopicRecord");
            modelBuilder.Entity<FollowRecord>().ToTable("FollowRecord");
            modelBuilder.Entity<IdentityRole>().ToTable("AspNetRoles");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("AspNetRoleClaims");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogins");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRoles");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserTokens");
        }
    }
}
