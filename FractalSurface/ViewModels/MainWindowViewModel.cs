using Avalonia.Controls;
using Avalonia.Interactivity;
using FractalSurface.Services;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.IO;
using System.Reactive;

namespace FractalSurface.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private Window _parrentWindow;

    private ViewModelBase _currentViewModel;

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            this.RaiseAndSetIfChanged(ref _currentViewModel, value);
        }
    }

    public RecentsViewModel RecentItemsViewModel { get; set; }

    public bool IsRecentsView { get => CurrentViewModel is RecentsViewModel; }

    public ReactiveCommand<Unit, Unit> OpenFilesCommand { get; }

    public ReactiveCommand<Unit, Unit> CloseFilesCommand { get; }

    

    public ReactiveCommand<RoutedEventArgs, Unit> OpenRecentFilesCommand { get; }

    public ReactiveCommand<CancelEventArgs, Unit> WindowClosingCommand { get; }

    public MainWindowViewModel(Window parrentWindow)
    {
        _parrentWindow = parrentWindow;

        OpenFilesCommand = ReactiveCommand.Create(OpenFiles);

        

        OpenRecentFilesCommand = ReactiveCommand.Create<RoutedEventArgs>(OpenRecentFiles);

        CloseFilesCommand = ReactiveCommand.Create(CloseFiles);

        WindowClosingCommand = ReactiveCommand.Create<CancelEventArgs>(OnClosing);

        RecentItemsViewModel = new RecentsViewModel();

        for (int i = 0; i < RecentItemsViewModel.RecentItems.Count; i++)
        {
            if (RecentItemsViewModel.RecentItems[i].Pictures.Count == 0)
                RecentItemsViewModel.RecentItems.Remove(RecentItemsViewModel.RecentItems[i]);
        }

        CurrentViewModel = RecentItemsViewModel;
    }



    private void CloseFiles()
    {
        RecentItemsViewModel.RecentItems.Reload();
        CurrentViewModel = RecentItemsViewModel;
    }

    private void OpenRecentFiles(RoutedEventArgs args)
    {
        if (CurrentViewModel is RecentsViewModel) 
        {
            var recentItemViewModel = RecentItemsViewModel.SelectedRecentItem;

            recentItemViewModel.OpenDate = DateTime.Now;

            CurrentViewModel = new WorkdeskViewModel(recentItemViewModel);
        }
    }

    private async void OpenFiles()
    {
        OpenFileDialog dialog = new OpenFileDialog() { AllowMultiple = true };
        
        dialog.Filters.Add(new FileDialogFilter() { Name = "Изображения", Extensions = { "png", "jpg", "jpeg", } });

        string[] fileNames = await dialog.ShowAsync(_parrentWindow);

        if (fileNames != null)
        {
            var recentItemViewModel = new RecentItemViewModel();

            recentItemViewModel.FilesDirectory = Path.GetDirectoryName(fileNames[0]);

            for (int i = 0; i < fileNames.Length; i++)
            {
                recentItemViewModel.Pictures.Add(new PictureViewModel() { FileName = Path.GetFileName(fileNames[i]) });
            }

            recentItemViewModel.SelectedPicture = recentItemViewModel.Pictures[0];

            recentItemViewModel.OpenDate = DateTime.Now;

            RecentItemsViewModel.RecentItems.Add(recentItemViewModel);

            CurrentViewModel = new WorkdeskViewModel(recentItemViewModel);
        }
    }

    private void OnClosing(CancelEventArgs args)
    {
        RecentsSerializer.Serialize(RecentItemsViewModel.RecentItems);   
    }
}
