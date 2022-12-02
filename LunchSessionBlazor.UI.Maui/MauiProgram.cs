using LunchSessionBlazor.UI.Extensions;
using Microsoft.Extensions.Logging;

namespace LunchSessionBlazor.UI.Maui
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
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

            //https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/local-web-services?view=net-maui-7.0#android
#if ANDROID
            builder.Configuration["ApiHost"] = "https://10.0.2.2:5101";
#else
            builder.Configuration["ApiHost"] = "https://localhost:5101";
#endif
            builder.Services.UseTodoApp(builder.Configuration,() => new MessageHandlerProvider().GetHttpMessageHandler());

            return builder.Build();
        }
    }
}