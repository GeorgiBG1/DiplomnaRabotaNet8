using Data.Models;

namespace CacheConfiguration
{
    public static class CacheData
    {
        public static List<Category> Categories { get; set; }
        public static List<City> Cities { get; set; }
        public static List<Skill> Skills { get; set; }
        public static List<int> Days { get; set; } //Schedule
    }
}
