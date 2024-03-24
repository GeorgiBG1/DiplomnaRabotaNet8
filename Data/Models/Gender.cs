namespace Data.Models
{
    public class Gender
    {
        public Gender(string Name, string BGName)
        {
            this.Name = Name;
            this.BGName = BGName;
        }
        public int Id { get; set; }
        public string Name { get; set; } = "None";
        public string BGName { get; set; } = "Няма";
    }
}
