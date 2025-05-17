using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using Photino.Blazor;

using PS5NorModifier.Shared;
using PS5NorModifier.Core;

namespace PS5NorModifier.Photino;

public class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var appBuilder = PhotinoBlazorAppBuilder.CreateDefault(args);

        appBuilder.Services.AddLogging();
        appBuilder.Services.AddMudServices();
        appBuilder.Services.AddBlazorWebView();
        appBuilder.Services.AddScoped<Test>();

        appBuilder.RootComponents.Add<App>("app");

        var app = appBuilder.Build();

        app.MainWindow
            .SetSize(1400, 800)
            .SetDevToolsEnabled(true)
            .SetLogVerbosity(0)
            //.SetIconFile("favicon.ico")
            .SetTitle("PS5NorModifier.Photino");

        AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
        {
            app.MainWindow.ShowMessage("Fatal exception", error.ExceptionObject.ToString());
        };

        app.Run();
    }
}

