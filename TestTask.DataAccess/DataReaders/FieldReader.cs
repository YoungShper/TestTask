using System.Globalization;
using System.Xml.Linq;
using TestTask.Core;
using TestTask.DataAccess.Interfaces;

namespace TestTask.DataAccess;

public class FieldReader : IDataReader<CustomPolygon>
{
    public async Task<List<CustomPolygon>> GetDataAsync(string path)
    {
        return await Task.Run(() =>
        {
            XDocument doc = XDocument.Load(path);
            var nameSpace = doc.Root?.GetDefaultNamespace();
            var placemarks = doc?.Descendants(nameSpace + "Placemark").ToList();
            var result = placemarks.Select(x =>
            {
                var simpleData = x.Descendants(nameSpace + "SimpleData").ToList();
                var points = x.Descendants(nameSpace + "coordinates").FirstOrDefault();
                var name = x.Descendants(nameSpace + "name").FirstOrDefault()?.Value;

                return new CustomPolygon
                {
                    ID = int.Parse(simpleData
                        .FirstOrDefault(data => data.Attribute("name")?.Value == "fid")
                        ?.Value),
                    Size = float.Parse(simpleData.FirstOrDefault(data => data.Attribute("name")?.Value == "size")?.Value),
                    Polygon = points.Value.Split(' ').Select(pointData =>
                    {
                        var coords = pointData.Split(',');
                        return new Coordinates { Lon = double.Parse(coords[0], CultureInfo.InvariantCulture), Lat = double.Parse(coords[1], CultureInfo.InvariantCulture) };
                    }).ToList(),
                    Name = name
                };
            }).ToList();

            return result;
        });
    }
}