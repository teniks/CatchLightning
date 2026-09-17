using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CatchLightning.Core.Abstractions.Presentation;
using CatchLightning.Core.Infrastructure;
using CatchLightning.features.Dashboard.Model;

namespace CatchLightning;

public partial class App : Application
{
    private readonly IMediator mediator = new Mediator();

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainVM = new MainWindowViewModel(mediator);
            desktop.MainWindow = new MainWindow() { DataContext = mainVM };
        }

        base.OnFrameworkInitializationCompleted();
    }
}