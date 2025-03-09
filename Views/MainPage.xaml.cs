using Vehicle_Maintenance_App.Configuration;
using Vehicle_Maintenance_App.ViewModels;

namespace Vehicle_Maintenance_App.Views
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel _mainPageViewModel;

        public MainPage(MainPageViewModel mainPageViewModel)
        {
            BindingContext = _mainPageViewModel = mainPageViewModel;
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
            _mainPageViewModel.LoadVehiclesCommand.Execute(null);
        }

        private void VehiclePicker_Change(object sender, EventArgs e) => _mainPageViewModel.ChangeVehicleCommand.Execute(null);
        private async void AddMaintenanceEvent_Click(object sender, EventArgs e) => await _mainPageViewModel.NavigateAbsoluteAsync(Constants.AddMaintenancePage);
        private async void ViewMaintenanceReport_Click(object sender, EventArgs e) => await _mainPageViewModel.CreateAndViewMaintenanceReportAsync();
        private async void EnterMaintenanceSchedule_Click(object sender, EventArgs e) => await _mainPageViewModel.NavigateAbsoluteAsync(Constants.AddMaintenanceSchedulePage);
        private async void EditVehicle_Click(object sender, EventArgs e) => await _mainPageViewModel.NavigateAbsoluteAsync(Constants.EditVehiclePage);
    }
}
