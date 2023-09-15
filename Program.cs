using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Rare_Yellow_Tigers.Models;
using System.Text.Json.Serialization;
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
    if (categoryToDelete == null)
    {
        return Results.NotFound();
    }
    db.Categories.Remove(categoryToDelete);
    return Results.NoContent();
});


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
    Tag tagToUpdate = db.Tags.Where(x => x.Id == id).FirstOrDefault();
    if (tagToUpdate == null)
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
    if (tagToDelete == null)
    {
        return Results.NotFound();
    }
    db.Tags.Remove(tagToDelete);
    db.SaveChanges();
    return Results.NoContent();
});


// Start of endpoints for Posts
app.MapGet("/api/posts", async (RareYellowTigersDbContext db) =>
{
    var posts = await db.Posts.ToListAsync();
    if (posts == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(posts);
});

app.MapGet("/api/posts/{id}", async (int id, RareYellowTigersDbContext db) =>
{
    var post = await db.Posts.FirstOrDefaultAsync(p => p.Id == id);

    if (post == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(post);

});

app.MapPost("/api/post", (RareYellowTigersDbContext db, Post post) =>
{
    try
    {
        db.Add(post);
        db.SaveChanges();
        return Results.Created($"/api/post/{post.Id}", post);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex);
    }
});

app.MapPut("/api/post{id}", (int id, RareYellowTigersDbContext db, Post post) =>
{
    var postToUpdate = db.Posts.Find(id);

    if (postToUpdate == null)
    {
        return Results.NotFound();
    }

    postToUpdate.RareUserId = post.RareUserId;
    postToUpdate.CategoryId = post.CategoryId;
    postToUpdate.Title = post.Title;
    postToUpdate.PublicationDate = post.PublicationDate;
    postToUpdate.ImageUrl = post.ImageUrl;
    postToUpdate.Content = post.Content;
    postToUpdate.IsApproved = post.IsApproved;

    db.SaveChanges();
    return Results.Ok(postToUpdate);

});

app.MapDelete("/api/post/{id}", (int id, RareYellowTigersDbContext db) =>
{
    var postToDelete = db.Posts.Find(id);

    if (postToDelete == null)
    {
        return Results.NotFound();
    }

    db.Posts.Remove(postToDelete);
    db.SaveChanges();
    return Results.NoContent();
});

//End of endpoints for Posts

// REACTIONS
// Create a new reaction
app.MapPost("/api/reactions", (RareYellowTigersDbContext db, Reaction reaction) =>
{
    db.Reactions.Add(reaction);
    db.SaveChanges();
    return Results.Created($"/api/comments/{reaction.Id}", reaction);
});

// USERS
// Getting all users
app.MapGet("/api/users", (RareYellowTigersDbContext db) =>
{
    return db.Users.ToList();
});

// Create a new user
app.MapPost("/api/users", (RareYellowTigersDbContext db, RareUser user) =>
{
    db.Users.Add(user);
    db.SaveChanges();
    return Results.Created($"/api/users/{user.Id}", user);
});

// Update an user
app.MapPut("/api/users/{userId}", (RareYellowTigersDbContext db, int id, RareUser user) =>
{
    RareUser userToUpdate = db.Users.SingleOrDefault(u => u.Id == id);
    if (userToUpdate == null)
    {
        return Results.NotFound();
    }
    userToUpdate.FirstName = user.FirstName;
    userToUpdate.LastName = user.LastName;
    userToUpdate.Email = user.Email;
    userToUpdate.Bio = user.Bio;
    userToUpdate.Uid = user.Uid;    
    userToUpdate.ProfileImageUrl = user.ProfileImageUrl;
    userToUpdate.CreatedOn = user.CreatedOn;
    userToUpdate.IsActive = user.IsActive;
    userToUpdate.IsStaff = user.IsStaff;

    db.SaveChanges();
    return Results.NoContent();
});

// Delete an user
app.MapDelete("/api/users/{userId}", (RareYellowTigersDbContext db, int id) =>
{
    RareUser user = db.Users.SingleOrDefault(u => u.Id == id);
    if (user == null)
    {
        return Results.NotFound();
    }
    db.Users.Remove(user);
    db.SaveChanges();
    return Results.NoContent();
});

// Get a single user profile
app.MapGet("/api/users/{userId}", (RareYellowTigersDbContext db, int id) =>
{
    RareUser user = db.Users.FirstOrDefault(u => u.Id == id);
    if (user == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(user);
});

// Get a current user's posts
app.MapGet("/api/users/{userId}/posts", (RareYellowTigersDbContext db, int id) =>
{
    RareUser user = db.Users.FirstOrDefault(u => u.Id == id);
    ICollection<Post> assignedPosts = db.Posts.Where(p => p.RareUserId == id).ToList();

    return assignedPosts;
});


app.Run();
