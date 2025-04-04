using Vehicle_Maintenance_App.Configuration;
using Vehicle_Maintenance_App.TempData;
using SQLite;
using Vehicle_Maintenance_App.Models;

namespace Vehicle_Maintenance_App.Data
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            if (!Temp.Data.TryGetValue(Constants.DatabaseKey, out object? db)) Task.Run(InitializeDatabaseAsync);
            else if (db != null) _database = (SQLiteAsyncConnection)db;
            else Task.Run(InitializeDatabaseAsync);
        }

        public async Task InitializeDatabaseAsync()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "vma.db");
            _database = new SQLiteAsyncConnection(dbPath);
            Temp.Data.Set(Constants.DatabaseKey, _database);
            await _database.CreateTableAsync<Vehicle>();
            await _database.CreateTableAsync<MaintenanceEvent>();
            await _database.CreateTableAsync<MaintenanceSchedule>();
            await _database.CreateTableAsync<MileageEntry>();
            if (await GetVehicleByIdAsync(Constants.AddVehicleObject.Id) == null)
                await AddVehicleAsync(Constants.AddVehicleObject);
        }

        #region Vehicle
        public Task<int> AddVehicleAsync(Vehicle vehicle)
        {
            return _database.InsertAsync(vehicle);
        }

        public async Task<List<Vehicle>> GetVehiclesAsync()
        {
            return await _database.Table<Vehicle>().ToListAsync();
        }

        public Task<Vehicle> GetVehicleByIdAsync(int id)
        {
            return _database.Table<Vehicle>().Where(v => v.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> DeleteVehicleAsync(int id)
        {
            // TODO: delete all references first
            return _database.Table<Vehicle>().DeleteAsync(v => v.Id == id);
        }
        #endregion

        #region MaintenanceEvent
        public Task<int> AddMaintenanceEventAsync(MaintenanceEvent mEvent)
        {
            return _database.InsertAsync(mEvent);
        }

        public async Task<List<MaintenanceEvent>> GetMaintenanceEventsAsync()
        {
            return await _database.Table<MaintenanceEvent>().ToListAsync();
        }

        public Task<List<MaintenanceEvent>> GetMaintenanceEventsByVehicleIdAsync(int vehicleId)
        {
            return _database.Table<MaintenanceEvent>().Where(v => v.VehicleId == vehicleId).ToListAsync();
        }
        #endregion
    }
}
