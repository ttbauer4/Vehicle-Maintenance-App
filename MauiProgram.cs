using Microsoft.Extensions.Logging;
using Vehicle_Maintenance_App.Data;
using Vehicle_Maintenance_App.Services;
using Vehicle_Maintenance_App.ViewModels;
using Vehicle_Maintenance_App.Views;

namespace Vehicle_Maintenance_App;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

        // Register services
        builder.Services.AddSingleton<DatabaseService>();
        builder.Services.AddSingleton<NavigationService>();
		builder.Services.AddSingleton<PdfService>();
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<AddVehiclePageViewModel>();
        builder.Services.AddSingleton<AddVehiclePage>();

        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Secrets.SyncfusionKey);

        return builder.Build();
	}
}
