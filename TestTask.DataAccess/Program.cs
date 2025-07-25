namespace TestTask.DataAccess;

class Program
{
    static void Main(string[] args)
    {
        FieldReader fieldReader = new FieldReader();
        CenterPointReader centerPointReader = new CenterPointReader();
        var ss = fieldReader.GetDataAsync("Data/fields.kml").Result;
        var aa = centerPointReader.GetDataAsync("Data/centroids.kml").Result;
        
        Console.ReadKey();
    }
}