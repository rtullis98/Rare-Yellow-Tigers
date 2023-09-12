using System.ComponentModel.DataAnnotations;


namespace Rare_Yellow_Tigers.Models;

public class Tag
{
    public int Id { get; set; }
    
    public string? Label { get; set; }
    
    public ICollection<Post> Posts { get; set; }

}
