namespace Data.Records
{
    public record ServiceStatus(int Id, string Name = null!) : BaseRecord
    {
        public static ServiceStatus None { get; } = new(0, "Няма");
        public static ServiceStatus Active { get; } = new(1, "Активна");
        public static ServiceStatus Inactive { get; } = new(2, "Неактивна");
        public static ServiceStatus VIP { get; } = new(3, "VIP");
        public override string ToString() => Name;
    }
}