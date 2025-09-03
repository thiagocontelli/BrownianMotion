using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using BrownianMotion.Views;
using BrownianMotion.ViewModels;

namespace BrownianMotion
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Inter-Regular.ttf", "InterRegular");
                    fonts.AddFont("Inter-SemiBold.ttf", "InterSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();

            return builder.Build();
        }
    }
}
