namespace DTOs.OUTPUT
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MainImage { get; set; }
        public int? ParentCategoryId { get; set; }
        public virtual CategoryDTO? ParentCategory { get; set; }
        public virtual ICollection<CategoryDTO>? Kids { get; set; }
        public virtual ICollection<ServiceCardDTO>? Services { get; set; }
    }
}
