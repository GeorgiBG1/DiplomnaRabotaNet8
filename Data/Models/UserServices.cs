﻿namespace DiplomnaRabotaNet8.Data.Models
{
    public class UserServices
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual Skiller User { get; set; }
        public int LaborServiceId { get; set; }
        public virtual LaborService LaborService { get; set; }
    }
}