namespace DTOs.OUTPUT
{
    public class ServiceCardDTO
    {
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public string? Description { get; set; }
        public string CategoryName { get; set; }
        public string MainImage { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
    }
}
