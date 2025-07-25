using TestTask.Core;

namespace TestTask.Logic.DTO;

public class FullLocationDTO
{
    public required Coordinates Center { get; set; }
    public required List<Coordinates> Polygon { get; set; }
}