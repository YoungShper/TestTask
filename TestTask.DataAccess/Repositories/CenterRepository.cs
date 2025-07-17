using TestTask.Core;
using TestTask.Core.Interfaces;

namespace TestTask.DataAccess.Repositories;

public class CenterRepository : ICenterRepository
{
    public Task<CenterPoint> GetCenterPointAsync(string path)
    {
        throw new NotImplementedException();
    }
}