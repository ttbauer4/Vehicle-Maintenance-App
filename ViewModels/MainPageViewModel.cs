using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Vehicle_Maintenance_App.Data;
using Vehicle_Maintenance_App.Models;
using CommunityToolkit.Mvvm.Input;
using Vehicle_Maintenance_App.Configuration;
using System.Windows.Input;
using Vehicle_Maintenance_App.Services;
using Vehicle_Maintenance_App.TempData;

namespace Vehicle_Maintenance_App.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseService _databaseService;
        private readonly NavigationService _navigationService;
        private readonly PdfService _pdfService;

        public ICommand LoadVehiclesCommand;
        public ICommand ChangeVehicleCommand;

        public ObservableCollection<Vehicle> Vehicles { get; } = new();

        private Vehicle _selectedVehicle;
        public Vehicle SelectedVehicle
        {
            get => _selectedVehicle;
            set
            {
                if (_selectedVehicle == null || !_selectedVehicle.Equals(value))
                {
                    _selectedVehicle = value;
                    OnPropertyChanged();
                }
                if (_selectedVehicle != null && !_selectedVehicle.Equals(Constants.AddVehicleObject))
                    Temp.Data.Set(Constants.CurrentVehicleKey, _selectedVehicle);
            }
        }

        private bool _buttonStackVisible;
        public bool ButtonStackVisible
        {
            get => _buttonStackVisible;
            set
            {
                if (!_buttonStackVisible.Equals(value))
                {
                    _buttonStackVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainPageViewModel(DatabaseService databaseService, NavigationService navigationService, PdfService pdfService)
        {
            _databaseService = databaseService;
            _navigationService = navigationService;
            _pdfService = pdfService;

            LoadVehiclesCommand = new AsyncRelayCommand(LoadVehicleListAsync);
            ChangeVehicleCommand = new AsyncRelayCommand(ChangeVehicleAsync);

            LoadVehiclesCommand.Execute(null);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task LoadVehicleListAsync()
        {
            List<Vehicle> vehicles = await _databaseService.GetVehiclesAsync();
            Vehicles.Clear();
            foreach (Vehicle v in vehicles) Vehicles.Add(v);
        }

        public async Task ChangeVehicleAsync()
        {
            if (SelectedVehicle == null) return;
            else if (SelectedVehicle.Id == Constants.AddVehicleObject.Id)
            {
                ButtonStackVisible = false;
                await NavigateAbsoluteAsync(Constants.AddVehiclePage);
            }
            else ButtonStackVisible = true;
        }

        public async Task NavigateAbsoluteAsync(string path)
        {
            await _navigationService.NavigateAbsolute(path);
        }

        public async Task CreateAndViewMaintenanceReportAsync()
        {
            string path = _pdfService.GenerateMaintenanceReport(SelectedVehicle);
            await _pdfService.Open(path);
        }
    }
}