using ServiceStack.DataAnnotations;
namespace Data.Models
{
    public class SkillBoxService : BaseEntity<int>
    {
        public SkillBoxService()
        {
            Reviews = new HashSet<Review>();
            Chats = new HashSet<Chat>();
        }
        [Unique]
        public string Name { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string MainImage { get; set; }
        public string Images { get; set; }
        public decimal Price { get; set; }
        public int VisitsCount { get; set; }
        public string? UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public string? Schedule { get; set; }
        public string? PhoneNumber { get; set; }
        public string? WebsiteURL { get; set; }
        public string? WebsiteName { get; set; }
        public string? OwnerName { get; set; }
        [Unique]
        public string OwnerId { get; set; }
        public virtual SkillBoxUser Owner { get; set; }
        public virtual City City { get; set; }
        public virtual ServiceStatus ServiceStatus { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
    }
}