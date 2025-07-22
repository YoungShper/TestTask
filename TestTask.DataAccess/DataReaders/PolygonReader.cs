using SharpKml.Base;
using SharpKml.Dom;
using SharpKml.Engine;
using TestTask.Core;
using TestTask.DataAccess.Interfaces;

namespace TestTask.DataAccess;

public class PolygonReader : IDataReader<CustomPolygon>
{
    public async Task<CustomPolygon> GetDataAsync(string path)
    {
        await Task.Run(() =>
        {
            var file = File.OpenRead(path);
            var parser = new Parser();
            parser.Parse(file, false);
            Kml kml = parser.Root as Kml;
            if (kml != null)
            {
                // Process the KML data
                foreach (var feature in kml.Flatten())
                {
                    if (feature is Placemark placemark)
                    {
                        Console.WriteLine($"Placemark: {placemark.Name}");
                        return new CustomPolygon{ID = 0, Points = new List<Coordinates>(), Size = 1};
                    }
                }
            }

            return null;

        });
        return null;
    }

}