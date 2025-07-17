using TestTask.Core;
using TestTask.Core.Interfaces;

namespace TestTask.Logic;

public class PointService
{
    private ICenterRepository _centerRepository;

    public PointService(ICenterRepository centerRepository)
    {
        _centerRepository = centerRepository;
    }

    public async Task<CenterPoint> GetAsync()
    {
        return await _centerRepository.GetCenterPointAsync();
    }
}