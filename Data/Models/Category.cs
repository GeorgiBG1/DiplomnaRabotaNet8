namespace Data.Models
{
    public class Category : BaseEntity<int>
    {
        public Category()
        {
            Kids = new HashSet<Category>();
            Services = new HashSet<SkillBoxService>();
        }
        public string Name { get; set; }
        public string MainImage { get; set; }
        public int? ParentCategoryId { get; set; }
        public virtual Category? ParentCategory { get; set; }
        public virtual ICollection<Category>? Kids { get; set; }
        public virtual ICollection<SkillBoxService>? Services { get; set; }
    }
}
