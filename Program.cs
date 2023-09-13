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
    db.SaveChanges();
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
