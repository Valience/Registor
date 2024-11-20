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

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<FormPage>();

        builder.Services.AddSingleton<FormPageViewModel>();
        builder.Services.AddSingleton<CryptoModuleViewModel>();

        builder.Services.AddSingleton<CryptoModuleService>();

        return builder.Build();
    }
}
