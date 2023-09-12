namespace Rare_Yellow_Tigers.Models;

public class Category
{
    public int Id { get; set; }
    public string? Label { get; set; }
   
    public ICollection<Post> Posts { get; set; }
}
