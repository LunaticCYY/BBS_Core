﻿using BBSTest.Models;
using Microsoft.EntityFrameworkCore;

namespace BBSTest.Data
{
    public class BBSContext : DbContext
    {
        public BBSContext(DbContextOptions<BBSContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Reply> Replys { get; set; }
        public DbSet<NodeRecord> NodeRecords { get; set; }
        public DbSet<TopicRecord> TopicRecords { get; set; }
        public DbSet<FollowRecord> FollowRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
