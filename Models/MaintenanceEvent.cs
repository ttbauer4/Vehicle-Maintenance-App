using SQLite;

namespace Vehicle_Maintenance_App.Models
{
    public class MaintenanceEvent
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public int VehicleId { get; set; }
        [NotNull]
        public string EventType { get; set; }
        public string? PartsUsed { get; set; } // encoded as a JSON list w/ each object containing {Name, Number, UnitCost, Quantity}
        public long? LaborCost { get; set; }
        public string? Description { get; set; }
        [NotNull]
        public string PerformedBy { get; set; }
        [NotNull]
        public DateTime PerformedTimestamp { get; set; }
        [NotNull]
        public DateTime InsertedTimestamp { get; set; }
        [NotNull]
        public DateTime LastUpdatedTimestamp { get; set; }
    }
}
