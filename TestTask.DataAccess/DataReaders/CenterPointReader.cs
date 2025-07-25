using System.Data;
using System.Globalization;
using System.Xml.Linq;
using TestTask.Core;
using TestTask.DataAccess.Interfaces;

namespace TestTask.DataAccess;

public class CenterPointReader : IDataReader<CenterPoint>
{
    public async Task<List<CenterPoint>> GetDataAsync(string path)
    {
        return await Task.Run(() =>
        {
            XDocument doc = XDocument.Load(path);
            var nameSpace = doc.Root?.GetDefaultNamespace();
            var placemarks = doc?.Descendants(nameSpace + "Placemark").ToList();
            
            var result = placemarks.Select(x =>
            {
                var simpleData = x.Descendants(nameSpace + "SimpleData").ToList();
                var points322 = x.Descendants(nameSpace + "coordinates").FirstOrDefault()?.Value;
                var points = x.Descendants(nameSpace + "coordinates").FirstOrDefault()?.Value.Split(',');
                var name = x.Descendants(nameSpace + "name").FirstOrDefault()?.Value;

                return new CenterPoint
                {
                    ID = int.Parse(simpleData
                        .FirstOrDefault(data => data.Attribute("name")?.Value == "fid")
                        ?.Value),
                    Size = float.Parse(
                        simpleData.FirstOrDefault(data => data.Attribute("name")?.Value == "size")?.Value),
                    Name = name,
                    Center = new Coordinates { Lon = double.Parse(points[0], CultureInfo.InvariantCulture), Lat = double.Parse(points[1], CultureInfo.InvariantCulture) }
                };
            }).ToList();

            return result;
        });
    }
}