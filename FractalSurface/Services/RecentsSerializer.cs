using FractalSurface.ViewModels;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Xml.Serialization;

namespace FractalSurface.Services;
public static class RecentsSerializer
{
    private static string filename = "Recents.xml";
    public static string BaseDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

    public static ObservableCollection<RecentItemViewModel> Reload(this ObservableCollection<RecentItemViewModel> recents)
    {
        recents.Serialize();
        return Deserialize();
    }

    public static void Serialize(this ObservableCollection<RecentItemViewModel> recents)
    {
        if(recents.Count > 0)
        {
            XmlSerializer xmlSerializerserializer = new XmlSerializer(typeof(ObservableCollection<RecentItemViewModel>));
            using (TextWriter writer = new StreamWriter(Path.Combine(BaseDir, filename)))
            {
                xmlSerializerserializer.Serialize(writer, recents);
            }
        }
    }

    public static ObservableCollection<RecentItemViewModel> Deserialize()
    {
        ObservableCollection<RecentItemViewModel>? recents = new ObservableCollection<RecentItemViewModel>();
        if (!File.Exists(Path.Combine(BaseDir, filename)))
        {
            recents.Serialize();
            return recents;
        }
        XmlSerializer xmlSerializerserializer = new XmlSerializer(typeof(ObservableCollection<RecentItemViewModel>));
        using (TextReader reader = new StreamReader(Path.Combine(BaseDir, filename)))
        {
            recents = xmlSerializerserializer.Deserialize(reader) as ObservableCollection<RecentItemViewModel>;
        }
        return recents;
    }
}