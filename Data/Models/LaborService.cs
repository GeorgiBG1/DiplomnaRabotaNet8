using ServiceStack.DataAnnotations;
namespace DiplomnaRabotaNet8.Data.Models
{
    public class LaborService
    {
        public int Id { get; set; }
        [Unique]
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string MainImage { get; set; }
        public string Images { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public virtual string? PhoneNumber { get; set; }
        public virtual string? WebsiteURL { get; set; }
        public virtual string? WebsiteName { get; set; }
    }
}