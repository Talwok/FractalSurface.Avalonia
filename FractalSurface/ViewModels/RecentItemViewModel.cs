using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace FractalSurface.ViewModels;
[Serializable]
public class RecentItemViewModel : ViewModelBase
{
    public ObservableCollection<string> FileNames { get; set; } = new ObservableCollection<string>();
    
    public string AllFileNames { get => FileNames.Aggregate((i, j) => i + ", " + j); }

    [NonSerialized]
    private string _filesDirectory;

    public string FilesDirectory
    {
        get => _filesDirectory;
        set => this.RaiseAndSetIfChanged(ref _filesDirectory, value);
    }
    public DateTime OpenDate { get; set; }
    public RecentItemViewModel()
    {
    }
}
