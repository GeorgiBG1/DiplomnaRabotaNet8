using ServiceStack.DataAnnotations;

namespace DiplomnaRabotaNet8.Data.Models
{
    public class LaborService
    {
        public int Id { get; set; }
        [Unique]
        public string Name { get; set; }
        public string AuthorId { get; set; }
        public virtual Skiller Author { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string MainImage { get; set; }
        public string Images { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string WebsiteUrl { get; set; } //TODO
        public bool IsCompany { get; set; } //TODO
    }
}