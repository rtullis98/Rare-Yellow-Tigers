using Rare_Yellow_Tigers.Models;

namespace Rare_Yellow_Tigers.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? UserName { get; set; }
        public string? Content { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Reaction> Reactions { get; set; }
        public DateTime PublicationDate { get; set; }
        public string? Category { get; set; }
        public List<string>? Tags { get; set; }

        public string? ImageUrl { get; set; }
    }
}
