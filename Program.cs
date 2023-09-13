using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Rare_Yellow_Tigers.Models;
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

app.Run();
