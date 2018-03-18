﻿// <auto-generated />
using BBS.Data;
using BBS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace BBS.Migrations
{
    [DbContext(typeof(BBSContext))]
    [Migration("20180318055037_20180318_Test")]
    partial class _20180318_Test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BBS.Models.FollowRecord", b =>
                {
                    b.Property<string>("FollowRecordId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime");

                    b.Property<string>("FollowId");

                    b.Property<string>("UserId");

                    b.HasKey("FollowRecordId");

                    b.HasIndex("UserId");

                    b.ToTable("FollowRecord");
                });

            modelBuilder.Entity("BBS.Models.Node", b =>
                {
                    b.Property<string>("NodeId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime");

                    b.Property<int>("IsParent");

                    b.Property<string>("Name");

                    b.Property<string>("ParentId");

                    b.Property<string>("UserId");

                    b.HasKey("NodeId");

                    b.HasIndex("UserId");

                    b.ToTable("Node");
                });

            modelBuilder.Entity("BBS.Models.NodeRecord", b =>
                {
                    b.Property<string>("NodeRecordId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime");

                    b.Property<string>("NodeId");

                    b.Property<string>("UserId");

                    b.HasKey("NodeRecordId");

                    b.HasIndex("NodeId");

                    b.HasIndex("UserId");

                    b.ToTable("NodeRecord");
                });

            modelBuilder.Entity("BBS.Models.Reply", b =>
                {
                    b.Property<string>("ReplyId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime");

                    b.Property<string>("Content");

                    b.Property<DateTime>("LastTime");

                    b.Property<string>("ParentId");

                    b.Property<string>("TopicId");

                    b.Property<string>("UserId");

                    b.HasKey("ReplyId");

                    b.HasIndex("TopicId");

                    b.HasIndex("UserId");

                    b.ToTable("Reply");
                });

            modelBuilder.Entity("BBS.Models.Topic", b =>
                {
                    b.Property<string>("TopicId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime");

                    b.Property<string>("Content");

                    b.Property<DateTime>("LastTime");

                    b.Property<string>("NodeId");

                    b.Property<string>("Title");

                    b.Property<string>("UserId");

                    b.HasKey("TopicId");

                    b.HasIndex("NodeId");

                    b.HasIndex("UserId");

                    b.ToTable("Topic");
                });

            modelBuilder.Entity("BBS.Models.TopicRecord", b =>
                {
                    b.Property<string>("TopicRecordId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime");

                    b.Property<string>("TopicId");

                    b.Property<string>("UserId");

                    b.HasKey("TopicRecordId");

                    b.HasIndex("TopicId");

                    b.HasIndex("UserId");

                    b.ToTable("TopicRecord");
                });

            modelBuilder.Entity("BBS.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<DateTime>("AddTime");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("Image");

                    b.Property<string>("Introduce");

                    b.Property<DateTime>("LastTime");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<int>("State");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("BBS.Models.FollowRecord", b =>
                {
                    b.HasOne("BBS.Models.User", "User")
                        .WithMany("FollowRecords")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("BBS.Models.Node", b =>
                {
                    b.HasOne("BBS.Models.User", "User")
                        .WithMany("Nodes")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("BBS.Models.NodeRecord", b =>
                {
                    b.HasOne("BBS.Models.Node", "Node")
                        .WithMany("NodeRecords")
                        .HasForeignKey("NodeId");

                    b.HasOne("BBS.Models.User", "User")
                        .WithMany("NodeRecords")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("BBS.Models.Reply", b =>
                {
                    b.HasOne("BBS.Models.Topic", "Topic")
                        .WithMany("Replys")
                        .HasForeignKey("TopicId");

                    b.HasOne("BBS.Models.User", "User")
                        .WithMany("Replys")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("BBS.Models.Topic", b =>
                {
                    b.HasOne("BBS.Models.Node", "Node")
                        .WithMany()
                        .HasForeignKey("NodeId");

                    b.HasOne("BBS.Models.User", "User")
                        .WithMany("Topics")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("BBS.Models.TopicRecord", b =>
                {
                    b.HasOne("BBS.Models.Topic", "Topic")
                        .WithMany("TopicRecords")
                        .HasForeignKey("TopicId");

                    b.HasOne("BBS.Models.User", "User")
                        .WithMany("TopicRecords")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
