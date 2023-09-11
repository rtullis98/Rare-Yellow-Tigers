namespace Rare_Yellow_Tigers.Models;

public class Comment
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public int PostId { get; set; }
    public string Content { get; set; }

   public DateTime CreatedOn { get; set; }

}
