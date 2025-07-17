using SharpKml.Engine;
using TestTask.Core;
using TestTask.Core.Interfaces;

namespace TestTask.DataAccess.Repositories;

public class PolygonRepository : IPolygonRepository
{
    public Task<Polygon> GetPolygonAsync(string path)
    {
        KmlFile.Load("YourKmlFile.kml");
    }

}