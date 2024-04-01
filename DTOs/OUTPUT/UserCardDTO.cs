namespace DTOs.OUTPUT
{
    public class UserCardDTO
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string ProfilePhoto { get; set; }
        public double ReviewAvgCoef { get; set; }
        public int ReviewsCount { get; set; }
        public IEnumerable<string> Skills { get; set; }
        public string City { get; set; }
        //TODO Career property
        //TODO ProfilePhoto property
    }
}
