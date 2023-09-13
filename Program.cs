using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Rare_Yellow_Tigers.Models;
using System.Text.Json.Serialization;
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

app.Run();
