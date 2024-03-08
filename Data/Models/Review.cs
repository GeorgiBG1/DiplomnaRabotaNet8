﻿using System.ComponentModel.DataAnnotations;
namespace Data.Models
{
    public class Review : BaseEntity<int>
    {
        public string UserId { get; set; }
        public virtual SkillBoxUser User { get; set; }
        public int ServiceId { get; set; }
        public virtual SkillBoxService Service { get; set; }
        public string Comment { get; set; }
        [Range(1, 5)]
        public int? RatingStars { get; set; }
    }
}
