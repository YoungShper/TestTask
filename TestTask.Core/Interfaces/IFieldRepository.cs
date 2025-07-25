namespace TestTask.Core.Interfaces;

public interface IFieldRepository
{
    public Task<List<CustomPolygon>> GetAllFieldsAsync(string path);
    public Task<CustomPolygon> GetFieldByIDAsync(string path, int id);
    
}