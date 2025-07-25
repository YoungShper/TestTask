namespace TestTask.Core.Interfaces;

public interface IFieldRepository
{
    Task<List<CustomPolygon>> GetAllFieldsAsync(string path);
    Task<CustomPolygon> GetFieldByIDAsync(string path, int id);
    
}