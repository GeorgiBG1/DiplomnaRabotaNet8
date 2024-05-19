﻿using DTOs.OUTPUT;

namespace Models
{
    public class SingleServiceViewModel
    {
        public ServiceDTO Service {  get; set; }
        public Dictionary<int, int> RatingStats { get; set; }
        public ICollection<CategoryMiniDTO> CategoryList { get; set; }
        public ICollection<ServiceCardDTO> ServiceCardDTOs { get; set; }
    }
}
