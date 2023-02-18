using FractalSurface.ViewModels;
using System.IO;
using System.Xml.Serialization;

namespace FractalSurface.Services;
public static class RecentsSerializer
{
    private static string filename = "Recents.xml";
    public static string BaseDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

    public static RecentsViewModel Reload(this RecentsViewModel recents)
    {
        recents.Serialize();
        return Deserialize();
    }

    public static void Serialize(this RecentsViewModel recents)
    {
        XmlSerializer xmlSerializerserializer = new XmlSerializer(typeof(RecentsViewModel));
        using (TextWriter writer = new StreamWriter(Path.Combine(BaseDir, filename)))
        {
            xmlSerializerserializer.Serialize(writer, recents);
        }
    }

    public static RecentsViewModel Deserialize()
    {
        RecentsViewModel? recents = new RecentsViewModel();
        if (!File.Exists(Path.Combine(BaseDir, filename)))
        {
            recents.Serialize();
            return recents;
        }
        XmlSerializer xmlSerializerserializer = new XmlSerializer(typeof(RecentsViewModel));
        using (TextReader reader = new StreamReader(Path.Combine(BaseDir, filename)))
        {
            recents = xmlSerializerserializer.Deserialize(reader) as RecentsViewModel;
        }
        return recents;
    }
}