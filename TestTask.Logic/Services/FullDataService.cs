using TestTask.Core.Interfaces;
using TestTask.Logic.DTO;
using TestTask.Logic.Helpers;
using TestTask.Logic.Interfaces;

namespace TestTask.Logic.Services;

public class FullDataService : IFullDataService
{
    private ICenterRepository _centerRepository;
    private IFieldRepository _fieldRepository;
    private IFullDataServiceOptions _options;
    private string _fieldsFilePath;
    private string _centersFilePath;

    public FullDataService(ICenterRepository centerRepository, IFieldRepository fieldRepository, IFullDataServiceOptions options)
    {
        _centerRepository = centerRepository;
        _fieldRepository = fieldRepository;
        _fieldsFilePath = options.FieldsPath;
        _centersFilePath = options.CentroidsPath;
    }

    public async Task<List<FullFieldData>> GetAllFullFieldsDataAsync()
    {
        var fields = await _fieldRepository.GetAllFieldsAsync(_fieldsFilePath);
        var centers = await _centerRepository.GetAllCentersAsync(_centersFilePath);

        var data = (from field in fields
            join center in centers on field.ID equals center.ID
            select new FullFieldData
            {
                ID = field.ID,
                Name = field.Name,
                Size = field.Size,
                Locations = new FullLocationData
                {
                    Center = center.Center,
                    Polygon = field.Polygon
                }
            }).ToList();
        return data;
    }

    public async Task<FieldSizeData> GetFieldSizeDataAsync(int fieldID)
    {
        var field = await _fieldRepository.GetFieldByIDAsync(_fieldsFilePath, fieldID);

        return new FieldSizeData()
        {
            Size = GeometryHelper.GetArea(field.Polygon)
        };
    }

    public async Task<DistanceData> GetDistanceToCenterAsync(int id, double latitude, double longitude)
    {
        var center = await _centerRepository.GetCenterPointByIDAsync(_centersFilePath, id);
        return new DistanceData
        {
            Distance = GeometryHelper.GetDistanceFromCenter(center.Center, latitude, longitude)
        };
    }

    public async Task<FieldByPointData> GetFieldByPointDataAsync(double latitude, double longitude)
    {
        var fields = await _fieldRepository.GetAllFieldsAsync(_fieldsFilePath);
        
        var result = fields
            .FirstOrDefault(x => 
                GeometryHelper.IsPointInsidePolygon(x.Polygon, latitude, longitude));
        if (result == null)
        {
            return new FieldByPointData();
        }

        return new FieldByPointData()
        {
            ID = result.ID,
            Name = result.Name,
            IsInField = true
        };
    }
}