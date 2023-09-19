﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Rare_Yellow_Tigers.Models;

#nullable disable

namespace RareYellowTigers.Migrations
{
    [DbContext(typeof(RareYellowTigersDbContext))]
    partial class RareYellowTigersDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PostReaction", b =>
                {
                    b.Property<int>("PostsId")
                        .HasColumnType("integer");

                    b.Property<int>("ReactionsId")
                        .HasColumnType("integer");

                    b.HasKey("PostsId", "ReactionsId");

                    b.HasIndex("ReactionsId");

                    b.ToTable("PostReaction");
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.Property<int>("PostsId")
                        .HasColumnType("integer");

                    b.Property<int>("TagsId")
                        .HasColumnType("integer");

                    b.HasKey("PostsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("PostTag");
                });

            modelBuilder.Entity("Rare_Yellow_Tigers.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Label")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Label = "Music"
                        },
                        new
                        {
                            Id = 2,
                            Label = "Movie"
                        });
                });

            modelBuilder.Entity("Rare_Yellow_Tigers.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("PostId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Rare_Yellow_Tigers.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("PublicationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("RareUserId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("RareUserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Rare_Yellow_Tigers.Models.PostReaction", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("integer");

                    b.Property<int>("ReactionId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasIndex("PostId");

                    b.HasIndex("ReactionId");

                    b.HasIndex("UserId");

                    b.ToTable("PostReactions");
                });

            modelBuilder.Entity("Rare_Yellow_Tigers.Models.RareUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsStaff")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProfileImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("Uid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Bio = "hard working blue collar man",
                            CreatedOn = new DateTime(2023, 9, 18, 19, 11, 15, 861, DateTimeKind.Local).AddTicks(3449),
                            Email = "papastone@rockville.net",
                            FirstName = "Fred",
                            IsActive = true,
                            IsStaff = false,
                            LastName = "Flintstone",
                            Uid = ""
                        },
                        new
                        {
                            Id = 2,
                            Bio = "just another hard working blue collar man",
                            CreatedOn = new DateTime(2023, 9, 18, 19, 11, 15, 861, DateTimeKind.Local).AddTicks(3460),
                            Email = "brubble@rockville.net",
                            FirstName = "Barny",
                            IsActive = true,
                            IsStaff = false,
                            LastName = "Rubble",
                            Uid = ""
                        });
                });

            modelBuilder.Entity("Rare_Yellow_Tigers.Models.Reaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Image_Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Reactions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ImageUrl = "👍",
                            Label = "Thumbs Up"
                        },
                        new
                        {
                            Id = 2,
                            ImageUrl = "👎",
                            Label = "Thumbs Down"
                        },
                        new
                        {
                            Id = 3,
                            ImageUrl = "💖",
                            Label = "Heart"
                        });
                });

            modelBuilder.Entity("Rare_Yellow_Tigers.Models.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("EndedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("FollowerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("FollowerId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("Rare_Yellow_Tigers.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Label")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Label = "Action"
                        },
                        new
                        {
                            Id = 2,
                            Label = "Rock"
                        },
                        new
                        {
                            Id = 3,
                            Label = "Drama"
                        });
                });

            modelBuilder.Entity("PostReaction", b =>
                {
                    b.HasOne("Rare_Yellow_Tigers.Models.Post", null)
                        .WithMany()
                        .HasForeignKey("PostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Rare_Yellow_Tigers.Models.Reaction", null)
                        .WithMany()
                        .HasForeignKey("ReactionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.HasOne("Rare_Yellow_Tigers.Models.Post", null)
                        .WithMany()
                        .HasForeignKey("PostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Rare_Yellow_Tigers.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Rare_Yellow_Tigers.Models.Comment", b =>
                {
                    b.HasOne("Rare_Yellow_Tigers.Models.RareUser", "Author")
                        .WithMany("Comments")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Rare_Yellow_Tigers.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Rare_Yellow_Tigers.Models.Post", b =>
                {
                    b.HasOne("Rare_Yellow_Tigers.Models.Category", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Rare_Yellow_Tigers.Models.RareUser", "RareUser")
                        .WithMany("Posts")
                        .HasForeignKey("RareUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("RareUser");
                });

            modelBuilder.Entity("Rare_Yellow_Tigers.Models.PostReaction", b =>
                {
                    b.HasOne("Rare_Yellow_Tigers.Models.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Rare_Yellow_Tigers.Models.Reaction", "Reaction")
                        .WithMany()
                        .HasForeignKey("ReactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Rare_Yellow_Tigers.Models.RareUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Reaction");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Rare_Yellow_Tigers.Models.Subscription", b =>
                {
                    b.HasOne("Rare_Yellow_Tigers.Models.RareUser", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Rare_Yellow_Tigers.Models.RareUser", "Follower")
                        .WithMany()
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Follower");
                });

            modelBuilder.Entity("Rare_Yellow_Tigers.Models.Category", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("Rare_Yellow_Tigers.Models.Post", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("Rare_Yellow_Tigers.Models.RareUser", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
