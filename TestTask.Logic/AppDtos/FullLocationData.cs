using TestTask.Core;

namespace TestTask.Logic.DTO;

public class FullLocationData
{
    public required Coordinates Center { get; set; }
    public required List<Coordinates> Polygon { get; set; }
}