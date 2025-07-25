using TestTask.Core;
using TestTask.Core.Interfaces;
using TestTask.DataAccess.Interfaces;

namespace TestTask.DataAccess.Repositories;

public class CenterRepository : ICenterRepository
{
    IDataReader<CenterPoint> _CentersReader;

    public CenterRepository(IDataReader<CenterPoint> centersReader)
    {
        _CentersReader = centersReader;
    }
    public async Task<CenterPoint> GetCenterPointByIDAsync(string path, int id)
    {
        var data = await GetAllCentersAsync(path);
        return data.FirstOrDefault(x => x.ID == id);
    }
    
    public async Task<List<CenterPoint>> GetAllCentersAsync(string path) =>
        await _CentersReader.GetDataAsync(path);
}