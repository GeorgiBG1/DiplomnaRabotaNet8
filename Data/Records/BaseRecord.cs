namespace Data.Records
{
    public record BaseRecord
    {
        public static T? GetById<T>(int id)
        {
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(T))
                {
                    var record = (T)property.GetValue(null)!;
                    if (record != null)
                    {
                        if (record.GetType().GetProperty("Id")!.GetValue(record) as int? == id)
                        {
                            return record;
                        }
                    }

                }
            }
            return default;
        }
        public static int GetLastId<T>()
        {
            var properties = typeof(T).GetProperties();
            if (properties.Length > 0)
            {
                var lastProperty = properties[properties.Length - 1];
                if (lastProperty.GetValue(null) is T record)
                {
                    var idProperty = typeof(T).GetProperty("Id");
                    if (idProperty != null && idProperty.PropertyType == typeof(int))
                    {
                        return (int)idProperty.GetValue(record)!;
                    }
                }
            }
            return 0;
        }
    }
}