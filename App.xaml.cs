using Vehicle_Maintenance_App.Data;

namespace Vehicle_Maintenance_App
{
    public partial class App : Application
    {
        private readonly DatabaseService _databaseService;

        public App(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
            Task.Run(async () => await _databaseService.InitializeDatabaseAsync());
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}