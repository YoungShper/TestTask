namespace TestTask.Core.Interfaces;

public interface ICenterRepository
{
    public Task<CenterPoint> GetCenterPointByIDAsync(string path,  int id);
    public Task<List<CenterPoint>> GetAllCentersAsync(string path);
}