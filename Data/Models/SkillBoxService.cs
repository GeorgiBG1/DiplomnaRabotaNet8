using Microsoft.EntityFrameworkCore.Diagnostics;
using ServiceStack.DataAnnotations;
namespace Data.Models
{
    public class SkillBoxService : BaseEntity<int>
    {
        public SkillBoxService()
        {
            Reviews = new HashSet<Review>();
            Chats = new HashSet<Chat>();
        }
        [Unique]
        public string Name { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string MainImage { get; set; }
        public string Images { get; set; }
        public decimal Price { get; set; }
        public int VisitsCount { get; set; }
        public string? UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public string? Schedule { get; set; }
        public string? PhoneNumber { get; set; }
        public string? WebsiteURL { get; set; }
        public string? WebsiteName { get; set; }
        public string? MainSkill { get; set; }
        public string? OwnerName { get; set; }
        [Unique]
        public string OwnerId { get; set; }
        public virtual SkillBoxUser Owner { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public int ServiceStatusId { get; set; }
        public virtual ServiceStatus ServiceStatus { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
        public bool[] DaysFromString(string schedule)
        {
            bool[] days = new bool[7];
            if (schedule != null)
            {
                var dayOptions = schedule.Split(", ").ToList();
                foreach (var day in dayOptions)
                {
                    if (day == "Понеделник")
                    {
                        days[0] = true;
                    }
                    else if (day == "Вторник")
                    {
                        days[1] = true;
                    }
                    else if (day == "Сряда")
                    {
                        days[2] = true;
                    }
                    else if (day == "Четвъртък")
                    {
                        days[3] = true;
                    }
                    else if (day == "Петък")
                    {
                        days[4] = true;
                    }
                    else if (day == "Събота")
                    {
                        days[5] = true;
                    }
                    else if (day == "Неделя")
                    {
                        days[6] = true;
                    }
                }
            }
            return days;
        }
    }
}