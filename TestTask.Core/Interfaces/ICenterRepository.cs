namespace TestTask.Core.Interfaces;

public interface ICenterRepository
{
    public Task<CenterPoint> GetCenterPointAsync();
}