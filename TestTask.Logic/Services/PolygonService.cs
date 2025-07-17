using TestTask.Core;
using TestTask.Core.Interfaces;

namespace TestTask.Logic;

public class PolygonService
{
    private IPolygonRepository _polygonRepository;

    public PolygonService(IPolygonRepository polygonRepository)
    {
        _polygonRepository = polygonRepository;
    }

    public async Task<Polygon> GetAsync()
    {
        return await _polygonRepository.GetPolygonAsync();
    }
}