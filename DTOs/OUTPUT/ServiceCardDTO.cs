namespace DTOs.OUTPUT
{
    public class ServiceCardDTO
    {
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public string CategoryName { get; set; }
        public string MainImage { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int ReviewsCount { get; set; }
    }
}
