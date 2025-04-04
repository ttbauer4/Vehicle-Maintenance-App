using Vehicle_Maintenance_App.Models;

namespace Vehicle_Maintenance_App.Configuration
{
    public class Constants
    {
        #region Vehicle Selection
        public const string AddVehicleString = "Add new vehicle...";
        public static Vehicle AddVehicleObject = new()
        {
            Id = 1,
            Make = string.Empty,
            Model = string.Empty,
            Year = 0,
            Name = AddVehicleString,
            InsertedTimestamp = DateTime.MinValue,
            LastUpdatedTimestamp = DateTime.MinValue
        };
        #endregion

        #region Page Routes
        public const string EditVehiclePage = "EditVehiclePage";
        public const string AddMaintenanceSchedulePage = "AddMaintenanceSchedulePage";
        public const string AddMaintenancePage = "AddMaintenancePage";
        public const string AddVehiclePage = "AddVehiclePage";
        public const string MainPage = "MainPage";
        #endregion

        #region Keys
        public const string DatabaseKey = "db";
        public const string CurrentVehicleKey = "CurrentVehicle";
        #endregion

        #region Common Strings
        public const string NotApplicable = "N/A";
        #endregion
    }
}
