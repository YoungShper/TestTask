namespace TestTask.Core.Interfaces;

public interface IPolygonRepository
{
    public Task<Polygon> GetPolygonAsync(string path);
}