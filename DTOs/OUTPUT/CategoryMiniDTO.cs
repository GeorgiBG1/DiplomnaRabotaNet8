namespace DTOs.OUTPUT
{
    public class CategoryMiniDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ServiceCardDTO>? Services { get; set; }
    }
}
