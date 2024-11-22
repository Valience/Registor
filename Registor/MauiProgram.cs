using Microsoft.Extensions.Logging;
using Registor.Services;
using Registor.View;
using Registor.ViewModel;

namespace Registor;
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

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<FormPage>();

        builder.Services.AddTransient<FormPageViewModel>();
        builder.Services.AddTransient<CryptoModuleViewModel>();

        builder.Services.AddSingleton<CryptoModuleService>();

        return builder.Build();
    }
}
