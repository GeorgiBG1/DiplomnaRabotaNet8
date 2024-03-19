namespace Data.Models
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; }
        public string MainImage { get; set; }
        public int? ParentCategoryId { get; set; }
        public virtual Category? ParentCategory { get; set; }
        public virtual ICollection<Category>? Kids { get; set; }
        public virtual ICollection<SkillBoxService>? Services { get; set; }
    }
}
