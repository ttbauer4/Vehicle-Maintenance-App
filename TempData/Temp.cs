namespace Vehicle_Maintenance_App.TempData
{
    public class Temp
    {
        public class DataStore<TKey, TValue> : Dictionary<TKey, TValue>
        {
            public void Set(TKey key, TValue value)
            {
                if (!base.ContainsKey(key)) base.Add(key, value);
                else this[key] = value;
            }
        }
        
        public static DataStore<string, object> Data { get; set; } = new();
    }
}
