using ServiceStack.DataAnnotations;

namespace Data.Models
{
    public class Offering : BaseEntity<string>
    {
        private bool isFinished = false;
        public decimal TotalPrice { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsFinished
        {
            get => isFinished;
            set
            {
                if (isFinished == false && value == true)
                {
                    isFinished = true;
                    FinishedOn = DateTime.UtcNow;
                }
            }
        }
        public DateTime? FinishedOn { get; private set; } = null;
        [Unique]
        public string UserId { get; set; }
        public virtual SkillBoxUser User { get; set; }
        public int ServiceId { get; set; }
        public virtual SkillBoxService Service { get; set; }
    }
}
