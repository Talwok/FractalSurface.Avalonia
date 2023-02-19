using Avalonia.Media.Imaging;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace FractalSurface.ViewModels;

[Serializable]
public class RecentItemViewModel : ViewModelBase
{
    public ObservableCollection<PictureViewModel> Pictures { get; set; } = new ObservableCollection<PictureViewModel>();

    public string FilesDirectory { get; set; }

    public DateTime OpenDate { get; set; }

    private PictureViewModel _selectedPicture;

    public PictureViewModel SelectedPicture
    {
        get => _selectedPicture;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedPicture, value);

            PictureName = new FileInfo(Path.Combine(FilesDirectory, SelectedPicture.FileName)).Name;

            this.RaisePropertyChanged(nameof(PictureName));

            PictureType = new FileInfo(Path.Combine(FilesDirectory, SelectedPicture.FileName)).Extension;

            this.RaisePropertyChanged(nameof(PictureType));

            if (FilesDirectory != null)
            {
                PictureResolution = new Bitmap(Path.Combine(FilesDirectory, SelectedPicture.FileName)).Size.ToString();

                this.RaisePropertyChanged(nameof(PictureResolution));
            }

            PictureLastWriteDate = new FileInfo(Path.Combine(FilesDirectory, SelectedPicture.FileName)).LastWriteTime;

            this.RaisePropertyChanged(nameof(PictureLastWriteDate));

            this.RaisePropertyChanged(nameof(Picture));
        }
    }

    private int _selectedPictureIndex;

    public int SelectedPictureIndex
    {
        get => _selectedPictureIndex;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedPictureIndex, value);
        }
    }

    public Bitmap Picture 
    {
        get => new Bitmap(Path.Combine(FilesDirectory, SelectedPicture.FileName));
    }

    public string PictureName { get; set; }

    public string PictureType { get; set; }
    
    public string PictureResolution { get; set; }
    
    public DateTime PictureLastWriteDate { get; set; }
    
    public string AllFileNames 
    {
        get 
        {
            List<string> names = new List<string>(); 
            
            for (int i = 0; i < Pictures.Count; i++)
            {
                names.Add(Pictures[i].FileName);
            }

            return names.Aggregate((i, j) => i + ", " + j);
        }
    }

    public RecentItemViewModel()
    {
        SelectedPictureIndex = 0;
    }
}
