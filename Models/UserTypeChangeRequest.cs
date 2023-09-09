using System.Security.Cryptography.X509Certificates;

namespace Rare_Yellow_Tigers.Models;

public class UserTypeChangeRequest
{
    public int Id { get; set; }
    public string Action { get; set; }
    public int Admin_One_Id { get; set; }
    public int Admin_Two_Id { get;set; }
    public int Modified_User_Id { get; set; }
}
