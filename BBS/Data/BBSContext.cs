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

        //public DbSet<User> Users { get; set; }
        //public DbSet<Node> Nodes { get; set; }
        //public DbSet<Topic> Topics { get; set; }
        //public DbSet<Reply> Replys { get; set; }
        //public DbSet<NodeRecord> NodeRecords { get; set; }
        //public DbSet<TopicRecord> TopicRecords { get; set; }
        //public DbSet<FollowRecord> FollowRecords { get; set; }

        public DbSet<User> User { get; set; }
        public DbSet<Node> Node { get; set; }
        public DbSet<Topic> Topic { get; set; }
        public DbSet<Reply> Reply { get; set; }
        public DbSet<NodeRecord> NodeRecord { get; set; }
        public DbSet<TopicRecord> TopicRecord { get; set; }
        public DbSet<FollowRecord> FollowRecord { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUserClaim<string>>().HasKey(p => new { p.UserId, p.Id });
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Node>().ToTable("Node");
            modelBuilder.Entity<Topic>().ToTable("Topic");
            modelBuilder.Entity<Reply>().ToTable("Reply");
            modelBuilder.Entity<NodeRecord>().ToTable("NodeRecord");
            modelBuilder.Entity<TopicRecord>().ToTable("TopicRecord");
            modelBuilder.Entity<FollowRecord>().ToTable("FollowRecord");
        }
    }
}
