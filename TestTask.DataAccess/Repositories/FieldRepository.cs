using TestTask.Core;
using TestTask.Core.Interfaces;
using TestTask.DataAccess.Interfaces;

namespace TestTask.DataAccess.Repositories;

public class FieldRepository : IFieldRepository
{
    IDataReader<CustomPolygon> _polygonReader;

    public FieldRepository(IDataReader<CustomPolygon> polygonReader)
    {
        _polygonReader = polygonReader;
    }
    
    public async Task<CustomPolygon> GetFieldByIDAsync(string path, int id)
    {
        var data = await GetAllFieldsAsync(path);
        return data.FirstOrDefault(x => x.ID == id);
    }
    
    public async Task<List<CustomPolygon>> GetAllFieldsAsync(string path) =>
        await _polygonReader.GetDataAsync(path);
}