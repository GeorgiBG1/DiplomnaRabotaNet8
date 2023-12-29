namespace DiplomnaRabotaNet8.Data.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public virtual Category? ParentCategory { get; set; }
        public virtual ICollection<Category>? Kids { get; set; }
        public virtual ICollection<LaborService>? Services { get; set; }
    }
}
