using Vehicle_Maintenance_App.Configuration;
using Vehicle_Maintenance_App.ViewModels;

namespace Vehicle_Maintenance_App.Views
{
    public partial class AddVehiclePage : ContentPage
    {
        private AddVehiclePageViewModel _addVehiclePageViewModel;

        public AddVehiclePage(AddVehiclePageViewModel addVehiclePageViewModel)
        {
            BindingContext = _addVehiclePageViewModel = addVehiclePageViewModel;
            InitializeComponent();
        }

        private async void Save_Click(object sender, EventArgs e) => await _addVehiclePageViewModel.SaveVehicleAsync();
        private async void Cancel_Click(object sender, EventArgs e) => await _addVehiclePageViewModel.NavigateAbsoluteAsync(Constants.MainPage);
    }
}
