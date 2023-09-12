namespace Rare_Yellow_Tigers.Models;

public class Subscription
{
    public int Id { get; set; }
    public int FollowerId { get; set; }
    public int AuthorId { get; set; }
    
    public DateTime CreatedOn { get; set; }
    public DateTime EndedOn { get; set;}
    
    public RareUser Author { get; set; }
    public RareUser Follower { get; set; }
   

    
}
