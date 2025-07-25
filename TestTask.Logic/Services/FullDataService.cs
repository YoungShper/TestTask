using TestTask.Core.Interfaces;
using TestTask.Logic.DTO;
using TestTask.Logic.Helpers;
using TestTask.Logic.Interfaces;

namespace TestTask.Logic.Services;

public class FullDataService : IFullDataService
{
    private ICenterRepository _centerRepository;
    private IFieldRepository _fieldRepository;
    private string _fieldsFilePath;
    private string _centersFilePath;

    public FullDataService(ICenterRepository centerRepository, IFieldRepository fieldRepository, string fieldsFilePath, string centersFilePath)
    {
        _centerRepository = centerRepository;
        _fieldRepository = fieldRepository;
        _fieldsFilePath = fieldsFilePath;
        _centersFilePath = centersFilePath;
    }

    public async Task<List<FullFieldDTO>> GetAllFullFieldsDataAsync()
    {
        var fields = await _fieldRepository.GetAllFieldsAsync(_fieldsFilePath);
        var centers = await _centerRepository.GetAllCentersAsync(_centersFilePath);

        var data = (from field in fields
            join center in centers on field.ID equals center.ID
            select new FullFieldDTO
            {
                ID = field.ID,
                Name = field.Name,
                Size = field.Size,
                Locations = new FullLocationDTO
                {
                    Center = center.Center,
                    Polygon = field.Polygon
                }
            }).ToList();
        return data;
    }

    public async Task<FieldSizeDTO> GetFieldSizeDataAsync(int fieldID)
    {
        var field = await _fieldRepository.GetFieldByIDAsync(_fieldsFilePath, fieldID);

        return new FieldSizeDTO()
        {
            Size = GeometryHelper.GetArea(field.Polygon)
        };
    }

    public async Task<DistanceDTO> GetDistanceToCenterAsync(int id, double latitude, double longitude)
    {
        var center = await _centerRepository.GetCenterPointByIDAsync(_centersFilePath, id);
        return new DistanceDTO
        {
            Distance = GeometryHelper.GetDistanceFromCenter(center.Center, latitude, longitude)
        };
    }

    public async Task<FieldByPointDTO> GetFieldByPointDataAsync(double latitude, double longitude)
    {
        var fields = await _fieldRepository.GetAllFieldsAsync(_fieldsFilePath);
        
        var result = fields
            .FirstOrDefault(x => 
                GeometryHelper.IsPointInsidePolygon(x.Polygon, latitude, longitude));
        if (result == null)
        {
            return new FieldByPointDTO();
        }

        return new FieldByPointDTO()
        {
            ID = result.ID,
            Name = result.Name,
            IsInField = true
        };
    }
}