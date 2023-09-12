namespace Rare_Yellow_Tigers.Models;

public class Post
{
    public int Id { get; set; }
    public int RareUserId { get; set; }
    public int CategoryId { get; set; }
    public string Title { get; set; }
    public DateTime PublicationDate { get; set; }
    public string? ImageUrl { get; set; }
    public string Content { get; set; }
    public bool IsApproved { get; set; }

 public ICollection<Comment> Comments { get; set; }
    
public ICollection<Tag> Tags { get; set; }

    public Category Category { get; set; }
    public ICollection<Reaction> Reactions { get; set; }
    public RareUser RareUser { get; set; }
}
