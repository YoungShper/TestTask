using TestTask.Logic.DTO;

namespace TestTask.Logic.Interfaces;

public interface IFullDataService
{
    Task<List<FullFieldData>> GetAllFullFieldsDataAsync();
    Task<FieldSizeData> GetFieldSizeDataAsync(int fieldID);
    Task<DistanceData> GetDistanceToCenterAsync(int id, double latitude, double longitude);
    Task<FieldByPointData> GetFieldByPointDataAsync(double latitude, double longitude);
}