using SQLite;

namespace Vehicle_Maintenance_App.Models
{
    public class Vehicle
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string Make { get; set; }
        [NotNull]
        public string Model { get; set; }
        [NotNull]
        public int Year { get; set; }
        [NotNull]
        public string Name { get; set; }
        public string? Vin { get; set; }
        public string? Notes { get; set; }
        [NotNull]
        public DateTime InsertedTimestamp { get; set; }
        [NotNull]
        public DateTime LastUpdatedTimestamp { get; set; }
    }
}
