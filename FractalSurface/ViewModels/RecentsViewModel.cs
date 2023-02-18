using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FractalSurface.ViewModels;

[Serializable]
public class RecentsViewModel : ViewModelBase
{
    public ObservableCollection<RecentItemViewModel> RecentItems { get; set; } = new ObservableCollection<RecentItemViewModel>();
}
