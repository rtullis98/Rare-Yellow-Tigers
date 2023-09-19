namespace Rare_Yellow_Tigers.Models;

public class PostReaction
{
    public Post Post { get; set; }
    public Reaction Reaction { get; set; }
    public RareUser User { get; set; }
};

