using Data.Models;
using ServiceStack.DataAnnotations;

namespace SkillBox.App.Data.Models
{
    public class UserChat
    {
        [Unique]
        public string Id { get; set; }
        public string UserId {  get; set; }
        public virtual SkillBoxUser User { get; set; }
        public string SkillBoxServiceId { get; set; }
        public virtual SkillBoxService SkillBoxService { get; set; }
    }
}