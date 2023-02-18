using Avalonia.Controls;
using DynamicData;
using FractalSurface.Services;
using FractalSurface.Views;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace FractalSurface.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private Window _parrentWindow;
    public ViewModelBase CurrentViewModel { get; set; }
    
    public RecentsViewModel RecentItemsViewModel { get; set; }

    public ReactiveCommand<Unit, Unit> OpenFilesCommand { get; }

    public ReactiveCommand<CancelEventArgs, Unit> WindowClosingCommand { get; }

    public MainWindowViewModel(Window parrentWindow)
    {
        _parrentWindow = parrentWindow;

        OpenFilesCommand = ReactiveCommand.Create(OpenFiles);

        WindowClosingCommand = ReactiveCommand.Create<CancelEventArgs>(OnClosing);

        RecentItemsViewModel = RecentsSerializer.Deserialize();
        
        CurrentViewModel = RecentItemsViewModel;

        
    }

    private async void OpenFiles()
    {
        OpenFileDialog dialog = new OpenFileDialog() { AllowMultiple = true };
        dialog.Filters.Add(new FileDialogFilter() { Name = "Изображения", Extensions = { "png", "jpg", "jpeg", } });

        string[] fileNames = await dialog.ShowAsync(_parrentWindow);

        if(fileNames != null)
        {
            var recentItemViewModel = new RecentItemViewModel();

            recentItemViewModel.FilesDirectory = Path.GetDirectoryName(fileNames[0]);

            for (int i = 0; i < fileNames.Length; i++)
            {
                recentItemViewModel.FileNames.Add(Path.GetFileName(fileNames[i]));
            }

            recentItemViewModel.OpenDate = DateTime.Now;

            RecentItemsViewModel.RecentItems.Add(recentItemViewModel);
        }
    }

    private void OnClosing(CancelEventArgs args)
    {
        RecentsSerializer.Serialize(RecentItemsViewModel);   
    }
}
