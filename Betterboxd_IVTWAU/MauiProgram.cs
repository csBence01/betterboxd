using Betterboxd_IVTWAU.Services;
using Betterboxd_IVTWAU.ViewModels;
using Betterboxd_IVTWAU.Views;
using Microsoft.Extensions.Logging;

namespace Betterboxd_IVTWAU
{
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
            builder.Services.AddSingleton<MovieDatabase>();
            builder.Services.AddTransient<MovieListViewModel>();
            builder.Services.AddTransient<MovieListPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
