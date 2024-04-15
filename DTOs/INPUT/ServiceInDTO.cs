using Data.Models;

namespace DTOs.INPUT
{
    public class ServiceInDTO
    {
        public ServiceInDTO()
        {
            Days = new bool[7];
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string? WebsiteName { get; set; }
        public decimal Price { get; set; }
        public string? UnitPrice { get; set; }
        public int Category { get; set; }
        public List<Category>? Categories { get; set; }
        public int City { get; set; }
        public List<City>? Cities { get; set; }
        public int Skill { get; set; }
        public List<Skill>? Skills { get; set; }
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


        public string Images { get; set; } = "none";
        public virtual IEnumerable<IFormFile> ImageFiles { get; set; }
    }
}
