namespace TestTask.Core.Interfaces;

public interface IPolygonRepository
{
    public Task<CustomPolygon> GetPolygonAsync();
}