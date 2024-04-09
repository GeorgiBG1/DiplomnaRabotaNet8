using Data.Models;

namespace DTOs.OUTPUT
{
    public class MessageDTO
    {
        public string Content { get; set; }
        public string CreatedOn { get; set; }
        public string UserFullName {  get; set; }
        public string UserProfilePhoto {  get; set; }
    }
}
