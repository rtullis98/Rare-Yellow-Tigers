using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Rare_Yellow_Tigers.Models;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Hosting;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<RareYellowTigersDbContext>(builder.Configuration["RareYellowTigersDbConnectionString"]);
// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//delete a post
app.MapDelete("/api/posts/{id}", (RareYellowTigersDbContext db, int id) =>
{
    Post post = db.Posts.SingleOrDefault(post => post.Id == id);
    if (post == null)
    {
        return Results.NotFound();
    }
    db.Posts.Remove(post);
    db.SaveChanges();
    return Results.NoContent();

});

//edit a post
app.MapPut("/posts/{id}", (RareYellowTigersDbContext db, int id, Post post) =>
{
    Post postToUpdate = db.Posts.FirstOrDefault(post => post.Id == id);
    if (postToUpdate == null)
    {
        return Results.NotFound();
    }

    if (id != post.Id)
    {
        return Results.BadRequest();
    }
    postToUpdate.Id = post.Id;
    db.SaveChanges();
    return Results.NoContent();
});


//app.UseAuthorization();

//app.MapControllers();


// COMMENTS
// Create a new comment
app.MapPost("/api/comments", (RareYellowTigersDbContext db, Comment comment) =>
{
    db.Comments.Add(comment);
    db.SaveChanges();
    return Results.Created($"/api/comments/{comment.Id}", comment);
});

// Update an existing comment
app.MapPut("/api/comments/{commentId}", (RareYellowTigersDbContext db, int id, Comment comment) =>
{
    Comment commentToUpdate = db.Comments.SingleOrDefault(c => c.Id == id);
    if (commentToUpdate == null)
    {
        return Results.NotFound();
    }
    commentToUpdate.AuthorId = comment.AuthorId;
    commentToUpdate.PostId = comment.PostId;
    commentToUpdate.Content = comment.Content;
    commentToUpdate.CreatedOn = comment.CreatedOn;
    db.SaveChanges();
    return Results.NoContent();
});

// Delete an existing comment 
app.MapDelete("/api/comments/{commentId}", (RareYellowTigersDbContext db, int id) =>
{
    Comment comment = db.Comments.SingleOrDefault(c => c.Id == id);
    if (comment == null)
    {
        return Results.NotFound();
    }

    db.Comments.Remove(comment);
    db.SaveChanges();
    return Results.NoContent();
});

//  ###   Category End Points   ###

//Get all Categories
app.MapGet("/api/catagories", (RareYellowTigersDbContext db) =>
{
    return db.Categories.ToList();
});

// Create Category
app.MapPost("/api/category", (RareYellowTigersDbContext db, Category category) =>
{
    db.Categories.Add(category);
    db.SaveChanges();
    return Results.Created($"/api/category/category.Id", category);
});

//Edit Category
app.MapPut("/api/category/{categoryId}", (RareYellowTigersDbContext db, Category category, int id) =>
{
  Category categoryToUpdate = db.Categories.Where(x => x.Id == id).FirstOrDefault();
  if (categoryToUpdate == null)
  {
    return Results.NotFound();
  }

  categoryToUpdate.Label = category.Label;

  db.SaveChanges();
	return Results.Created($"/api/category/category.Id", category);
});


//Delete Category
app.MapDelete("/api/category/{categoryId}", (RareYellowTigersDbContext db, int id) =>
{
    var categoryToDelete = db.Categories.Where(x => x.Id == id).FirstOrDefault();
    if(categoryToDelete == null)
    {
        return Results.NotFound();
    }
    db.Categories.Remove(categoryToDelete);
  

//  #### TAG Endpoints ####

//Get all Tags
app.MapGet("/api/tags", (RareYellowTigersDbContext db) =>
{
    return db.Tags.ToList();
});

//Create a Tag
app.MapPost("/api/tag", (RareYellowTigersDbContext db, Tag tag) =>
{
    db.Tags.Add(tag);
    db.SaveChanges();
    return Results.Created($"/api/tag", tag);
});

//Edit a Tag
app.MapPut("/api/tag/{tagId}", (RareYellowTigersDbContext db, Tag tag, int id) =>
{
    Tag tagToUpdate = db.Tags.Where(x =>x.Id == id).FirstOrDefault();
    if(tagToUpdate == null)
    {
        return Results.NotFound();
    }
    
    tagToUpdate.Label = tag.Label;
    db.SaveChanges();
    return Results.Created($"/api/tag/tag.Id", tag);

});

//Delete a Tag
app.MapDelete("/api/tag/{tagId}", (RareYellowTigersDbContext db, int id) =>
{
    var tagToDelete = db.Tags.Where(x => x.Id == id).FirstOrDefault();
    if(tagToDelete == null)
    {
        return Results.NotFound();
    }
    db.Tags.Remove(tagToDelete);
    db.SaveChanges();
    return Results.NoContent();
});

app.Run();
