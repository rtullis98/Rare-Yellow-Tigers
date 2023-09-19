using System.Security.Cryptography.X509Certificates;

namespace Rare_Yellow_Tigers.Models;

public class Comment
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public int PostId { get; set; }
    public string Content { get; set; }

   public DateTime CreatedOn { get; set; }

    public RareUser Author { get; set; }  
    public Post Post { get; set; }

    public Comment()
    {
        this.CreatedOn = DateTime.Now;
    }

}
