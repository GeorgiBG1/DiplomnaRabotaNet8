using Data.Models;
namespace DTOs.OUTPUT
{
    public class ServiceUpdateDTO
    {
        public ServiceUpdateDTO()
        {
            Days = new bool[7];
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string? WebsiteName { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string? UnitPrice { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int CityId { get; set; }
        public City? City { get; set; }
        public string? MainSkill { get; set; }
        public int SkillId { get; set; }
        public List<Skill>? Skills { get; set; }
        public SkillBoxUser? User { get; set; }
        public int StatusId { get; set; }
        public ServiceStatus? Status { get; set; }
        public bool[] Days { get; set; } //schedule

        public string DaysAsString()
        {
            string schedule = "";
            if (!Days.Any(d => d))
            {
                schedule = "Без график";
                return schedule;
            }
            else
            {
                if (Days[0])
                {
                    schedule += ", Понеделник";
                }
                if (Days[1])
                {
                    schedule += ", Вторник";
                }
                if (Days[2])
                {
                    schedule += ", Сряда";
                }
                if (Days[3])
                {
                    schedule += ", Четвъртък";
                }
                if (Days[4])
                {
                    schedule += ", Петък";
                }
                if (Days[5])
                {
                    schedule += ", Събота";
                }
                if (Days[6])
                {
                    schedule += ", Неделя";
                }
            }

            return schedule.TrimStart(',').TrimStart();
        }
        public string? MainImage { get; set; }
        public string? Images { get; set; }
    }
}
