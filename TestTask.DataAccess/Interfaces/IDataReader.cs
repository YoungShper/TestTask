using TestTask.Core;

namespace TestTask.DataAccess.Interfaces;

public interface IDataReader<T> where T : PointPolygonBase
{
    public Task<T> GetDataAsync(string path);
}