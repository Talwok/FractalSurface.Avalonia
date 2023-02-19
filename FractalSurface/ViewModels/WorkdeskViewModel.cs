using Avalonia.Media.Imaging;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.IO;
using System.Threading.Tasks;

namespace FractalSurface.ViewModels;
public class WorkdeskViewModel : ViewModelBase
{
    public RecentItemViewModel RecentItem { get; set; }

    public ReactiveCommand<Unit, Unit> CheckAllCommand { get; }

    public ReactiveCommand<Unit, Unit> UncheckAllCommand { get; }

    public ReactiveCommand<Unit, Unit> RemoveElementCommand { get; }

    public ReactiveCommand<Unit, Unit> MoveBackwardCommand { get; }

    public ReactiveCommand<Unit, Unit> MoveForwardCommand { get; }

    public WorkdeskViewModel(RecentItemViewModel recentItemViewModel)
	{
        RecentItem = recentItemViewModel;

        CheckAllCommand = ReactiveCommand.Create(CheckAll);

        UncheckAllCommand = ReactiveCommand.Create(UncheckAll);

        RemoveElementCommand = ReactiveCommand.Create(RemoveElement);

        MoveBackwardCommand = ReactiveCommand.Create(MoveBackward);

        MoveForwardCommand = ReactiveCommand.Create(MoveForward);
    }

    private void MoveBackward()
    {
        if (RecentItem.SelectedPictureIndex != 0)
        {
            RecentItem.SelectedPictureIndex--;
        }
        else
        {
            RecentItem.SelectedPictureIndex = RecentItem.Pictures.Count - 1;
        }
        RecentItem.RaisePropertyChanged("SelectedPictureIndex");
    }

    private void MoveForward()
    {
        if(RecentItem.SelectedPictureIndex != RecentItem.Pictures.Count - 1)
        {
            RecentItem.SelectedPictureIndex++;
        }
        else
        {
            RecentItem.SelectedPictureIndex = 0;
        }
        RecentItem.RaisePropertyChanged("SelectedPictureIndex");
    }

    private void RemoveElement()
    {
        RecentItem.Pictures.Remove(RecentItem.SelectedPicture);
        RecentItem.RaisePropertyChanged("Pictures");
        RecentItem.SelectedPicture = RecentItem.Pictures[0];
    }

    private void CheckAll()
    {
        for (int i = 0; i < RecentItem.Pictures.Count; i++)
        {
            RecentItem.Pictures[i].IsPictureChecked = true;
            RecentItem.Pictures[i].RaisePropertyChanged("IsPictureChecked");
        }
    }

    private void UncheckAll()
    {
        for (int i = 0; i < RecentItem.Pictures.Count; i++)
        {
            RecentItem.Pictures[i].IsPictureChecked = false;
            RecentItem.Pictures[i].RaisePropertyChanged("IsPictureChecked");
        }
    }
}
