namespace TestTask.Core.Interfaces;

public interface ICenterRepository
{
    Task<CenterPoint> GetCenterPointByIDAsync(string path,  int id);
    Task<List<CenterPoint>> GetAllCentersAsync(string path);
}