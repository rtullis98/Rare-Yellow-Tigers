namespace Rare_Yellow_Tigers.Models;

public class Reaction
{
    public int Id { get; set; }
    public string Label { get; set; }
    public string Image_Url { get; set; }

    public ICollection<Post> Posts { get; set; }

  
    

}
