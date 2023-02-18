using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using FractalSurface.ViewModels;
using FractalSurface.Views;

namespace FractalSurface;
public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
            var viewModel = new MainWindowViewModel(desktop.MainWindow);
            desktop.MainWindow.DataContext = viewModel;
        }

        base.OnFrameworkInitializationCompleted();
    }
}