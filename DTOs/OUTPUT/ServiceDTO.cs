namespace DTOs.OUTPUT
{
    public class ServiceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OwnerUsername { get; set; }
        public string OwnerName { get; set; }
        public string OwnerCareer { get; set; }
        public string OwnerProfilePhoto { get; set; }
        public string OwnerCurrentLocation { get; set; }
        public string? Description { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string? Schedule { get; set; }
        public string Location { get; set; }
        public string MainSkill { get; set; }
        public int ReviewsCount { get; set; }
        public double ReviewAvgCoef { get; set; }
        public ICollection<ReviewDTO> Reviews { get; set; }
        public ICollection<string> Images { get; set; }
        public virtual string? PhoneNumber { get; set; }
        public virtual string? WebsiteURL { get; set; }
        public virtual string? WebsiteName { get; set; }
    }
}
