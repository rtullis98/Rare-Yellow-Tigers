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

app.Run();
