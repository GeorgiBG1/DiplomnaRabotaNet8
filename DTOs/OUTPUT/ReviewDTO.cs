namespace DTOs.OUTPUT
{
    public class ReviewDTO
    {
        public string Content { get; set; }
        public string CreatedOn { get; set; }
        public int StarsCount { get; set; }
        public virtual UserMiniDTO User { get; set; }
    }
}
