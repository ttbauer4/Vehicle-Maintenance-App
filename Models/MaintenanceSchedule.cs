using SQLite;

namespace Vehicle_Maintenance_App.Models
{
    public class MaintenanceSchedule
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public int VehicleId { get; set; }
        [NotNull]
        public string EventType { get; set; }
        [NotNull]
        public string Intervals { get; set; } // encoded as a JSON list w/ each object containing {Mileage, MaintenanceEventId OR empty}
        [NotNull]
        public DateTime InsertedTimestamp { get; set; }
        [NotNull]
        public DateTime LastUpdatedTimestamp { get; set; }
    }
}
