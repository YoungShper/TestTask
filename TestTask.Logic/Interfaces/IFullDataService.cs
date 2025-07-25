using TestTask.Logic.DTO;

namespace TestTask.Logic.Interfaces;

public interface IFullDataService
{
    Task<List<FullFieldDTO>> GetAllFullFieldsDataAsync();
    Task<FieldSizeDTO> GetFieldSizeDataAsync(int fieldID);
    Task<DistanceDTO> GetDistanceToCenterAsync(int id, double latitude, double longitude);
    Task<FieldByPointDTO> GetFieldByPointDataAsync(double latitude, double longitude);
}