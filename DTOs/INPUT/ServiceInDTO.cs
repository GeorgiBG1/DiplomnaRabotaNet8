using Data.Models;

namespace DTOs.INPUT
{
    public class ServiceInDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string? WebsiteName { get; set; }
        public decimal Price { get; set; }
        public string? UnitPrice { get; set; }
        public int Category { get; set; }
        public List<Category> Categories { get; set; }
        public int City { get; set; }
        public List<City> Cities { get; set; }
        public int Skill { get; set; }
        public List<Skill> Skills { get; set; }
        public List<int> Days { get; set; } //Schedule
        public string Images { get; set; } = "none";
        public virtual IEnumerable<IFormFile> ImageFiles {  get; set; } 
    }
}
