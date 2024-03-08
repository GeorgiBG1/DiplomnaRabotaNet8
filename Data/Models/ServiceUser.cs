using Microsoft.EntityFrameworkCore;

namespace Data.Models
{
    [PrimaryKey(nameof(ServiceId), nameof(UserId))]
    public class ServiceUser
    {
        public string UserId { get; set; }
        public virtual SkillBoxUser User { get; set; }
        public int ServiceId { get; set; }
        public virtual SkillBoxService Service { get; set; }
    }
}