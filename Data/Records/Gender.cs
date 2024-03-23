namespace Data.Records
{
    public record Gender(int Id = 0, string Name = null!) : BaseRecord
    {
        public static Gender Unknown { get; } = new(0, "Няма");
        public static Gender Male { get; } = new(1, "Мъж");
        public static Gender Female { get; } = new(2, "Жена");
        public static Gender Other { get; } = new(3, "Друго");
        public override string ToString() => Name;
    }
}