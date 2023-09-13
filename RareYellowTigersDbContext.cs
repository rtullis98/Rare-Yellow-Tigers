﻿using Microsoft.EntityFrameworkCore;
namespace Rare_Yellow_Tigers.Models;

public class RareYellowTigersDbContext : DbContext
{
    public DbSet<RareUser> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Reaction> Reactions { get; set; }

    public RareYellowTigersDbContext(DbContextOptions<RareYellowTigersDbContext> context) : base(context)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<RareUser>().HasData(new RareUser[]
        {
        new RareUser {Id = 1, FirstName="Fred", LastName="Flintstone", Bio="hard working blue collar man", Uid="", Email="papastone@rockville.net", IsActive=true, IsStaff=false, CreatedOn=DateTime.Now},
        new RareUser {Id = 2, FirstName="Barny", LastName="Rubble", Bio="just another hard working blue collar man", Uid="", Email="brubble@rockville.net", IsActive=true, IsStaff=false, CreatedOn=DateTime.Now}

        });

        modelBuilder.Entity<Category>().HasData(new Category[]
       {
        new Category {Id = 1, Label="Music" },
        new Category {Id = 2, Label="Movie"}

       });

        modelBuilder.Entity<Reaction>().HasData(new Reaction[]
       {
        new Reaction {Id = 1, Label="Thumbs Up", Image_Url="👍" },
        new Reaction {Id = 2, Label="Thumbs Down", Image_Url="👎"},
        new Reaction {Id = 3, Label="Heart", Image_Url="💖"},

       });

        modelBuilder.Entity<Tag>().HasData(new Tag[]
       {
        new Tag {Id = 1, Label="Action" },
        new Tag {Id = 2, Label="Rock"},
        new Tag {Id = 3, Label="Drama"},
       });

        modelBuilder.Entity<Comment>().HasData(new Comment[]
        {
            new Comment {Id = 1, AuthorId = 1, PostId  = 1, Content = "What is dis?", CreatedOn = new DateTime(2021, 1, 2)},
            new Comment {Id = 2, AuthorId = 2, PostId  = 2, Content = "Werkkkkkk", CreatedOn = new DateTime(2022, 1, 2)},
            new Comment {Id = 3, AuthorId = 3, PostId  = 3, Content = "Looks delicious", CreatedOn = new DateTime(2023, 1, 2)},
        });
    }


}
