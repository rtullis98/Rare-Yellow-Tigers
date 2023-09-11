namespace Rare_Yellow_Tigers.Models;

public class RareUsers
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Bio { get; set; }
    public string Uid { get; set; }
    public string? ProfileImageUrl { get; set; }
    public DateTime? CreatedOn { get; set; }
    public bool IsActive { get; set; }

    public bool IsStaff { get; set; }
   

}
