namespace DTOs.OUTPUT
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string ProfilePhoto { get; set; }
        public string Location { get; set; }
        public string JoinedOn { get; set; }
        public string Gender { get; set; }
        public string? Career { get; set; }
        public string? Bio { get; set; }
        public string? WebsiteName { get; set; }
        public ICollection<string> Skills { get; set; }
    }
}
