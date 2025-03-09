using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Vehicle_Maintenance_App.Configuration;
using Vehicle_Maintenance_App.Data;
using Vehicle_Maintenance_App.Models;
using Vehicle_Maintenance_App.Services;

namespace Vehicle_Maintenance_App.ViewModels
{
    public class AddVehiclePageViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseService _databaseService;
        private readonly NavigationService _navigationService;

        public ICommand SaveVehicleCommand;

        private string? _make;
        public string? Make
        {
            get => _make;
            set
            {
                if (_make == null || !_make.Equals(value))
                {
                    _make = value;
                    OnPropertyChanged();
                    TrySuggestName();
                }
            }
        }

        private string? _model;
        public string? Model
        {
            get => _model;
            set
            {
                if (_model == null || !_model.Equals(value))
                {
                    _model = value;
                    OnPropertyChanged();
                    TrySuggestName();
                }
            }
        }

        private string? _year;
        public string? Year
        {
            get => _year;
            set
            {
                if (_year == null || !_year.Equals(value))
                {
                    _year = value;
                    OnPropertyChanged();
                    TrySuggestName();
                }
            }
        }

        private bool _isManualName;
        private bool _isNameFocused;
        public bool IsNameFocused
        {
            get => _isNameFocused;
            set
            {
                if (!_isNameFocused.Equals(value))
                {
                    _isNameFocused = value;
                    _isManualName |= _isNameFocused;
                    OnPropertyChanged();
                }
            }
        }

        private string? _name;
        public string? Name
        {
            get => _name;
            set
            {
                if (_name == null || !_name.Equals(value))
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string? _vin;
        public string? Vin
        {
            get => _vin;
            set
            {
                if (_vin == null || !_vin.Equals(value))
                {
                    _vin = value;
                    OnPropertyChanged();
                }
            }
        }

        private string? _notes;
        public string? Notes
        {
            get => _notes;
            set
            {
                if (_notes == null || !_notes.Equals(value))
                {
                    _notes = value;
                    OnPropertyChanged();
                }
            }
        }

        private string? _errorMessage;
        public string? ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (_errorMessage == null || !_errorMessage.Equals(value))
                {
                    _errorMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public AddVehiclePageViewModel(DatabaseService databaseService, NavigationService navigationService)
        {
            _databaseService = databaseService;
            _navigationService = navigationService;

            SaveVehicleCommand = new AsyncRelayCommand(SaveVehicleAsync);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task SaveVehicleAsync()
        {
            if (string.IsNullOrWhiteSpace(Make)
                || string.IsNullOrWhiteSpace(Model)
                || string.IsNullOrWhiteSpace(Name)
                || string.IsNullOrWhiteSpace(Year) || !int.TryParse(Year, out _))
            {
                ErrorMessage = "Please enter a valid Make, Model, Year, and Name for your vehicle.";
                return;
            }

            Vehicle newVehicle = new()
            {
                Make = Make,
                Model = Model,
                Vin = Vin,
                Year = int.TryParse(Year, out int year) ? year : 0,
                Name = Name,
                Notes = Notes
            };
            await _databaseService.AddVehicleAsync(newVehicle);
            await NavigateAbsoluteAsync(Constants.MainPage);
        }

        public async Task NavigateAbsoluteAsync(string path)
        {
            ClearForm();
            await _navigationService.NavigateAbsolute(path);
        }

        private void ClearForm()
        {
            Make = _make = null;
            Model = _model = null;
            Year = _year = null;
            IsNameFocused = _isNameFocused = _isManualName = false;
            Name = _name = null;
            Vin = _vin = null;
            Notes = _notes = null;
            ErrorMessage = _errorMessage = null;
        }

        private void TrySuggestName()
        {
            if (!_isManualName && Year != null && Make != null && Model != null)
            {
                Name = $"{Year} {Make} {Model}";
            }
        }
    }
}
