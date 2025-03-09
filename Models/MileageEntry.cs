using SQLite;

namespace Vehicle_Maintenance_App.Models
{
    public class MileageEntry
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public int VehicleId { get; set; }
        [NotNull]
        public int Mileage { get; set; }
        public string? Notes { get; set; }
        [NotNull]
        public DateTime ObservedTimestamp { get; set; }
        [NotNull]
        public DateTime InsertedTimestamp { get; set; }
        [NotNull]
        public DateTime LastUpdatedTimestamp { get; set; }
    }
}
