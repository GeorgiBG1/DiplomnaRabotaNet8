namespace Data.Records
{
    public record SkillLevel(int Id, string Name = null!) : BaseRecord
    {
        public static SkillLevel None { get; } = new(0, "0%");
        public static SkillLevel Level1 { get; } = new(1, "10%");
        public static SkillLevel Level2 { get; } = new(2, "20%");
        public static SkillLevel Level3 { get; } = new(3, "30%");
        public static SkillLevel Level4 { get; } = new(4, "40%");
        public static SkillLevel Level5 { get; } = new(5, "50%");
        public static SkillLevel Level6 { get; } = new(6, "60%");
        public static SkillLevel Level7 { get; } = new(7, "70%");
        public static SkillLevel Level8 { get; } = new(8, "80%");
        public static SkillLevel Level9 { get; } = new(9, "90%");
        public static SkillLevel Level10 { get; } = new(10, "100%");
        public override string ToString() => Name;
    }
}
