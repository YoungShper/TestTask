using System.Data;
using TestTask.Core;
using TestTask.DataAccess.Interfaces;

namespace TestTask.DataAccess;

public class CenterPointReader : IDataReader<CenterPoint>
{
    public Task<CenterPoint> GetDataAsync(string path)
    {
        throw new NotImplementedException();
    }
}