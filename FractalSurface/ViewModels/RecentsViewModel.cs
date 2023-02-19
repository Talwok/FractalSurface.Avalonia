using FractalSurface.Services;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;

namespace FractalSurface.ViewModels;

public class RecentsViewModel : ViewModelBase
{
    public ObservableCollection<RecentItemViewModel> RecentItems { get; set; } = RecentsSerializer.Deserialize();

    public RecentItemViewModel SelectedRecentItem { get; set; }

    public ReactiveCommand<Unit, Unit> ClearRecentsCommand { get; }

    public RecentsViewModel()
    {
        ClearRecentsCommand = ReactiveCommand.Create(ClearRecents);
    }

    private void ClearRecents()
    {
        RecentItems.Clear();
        RecentItems.Reload();
    }
}
