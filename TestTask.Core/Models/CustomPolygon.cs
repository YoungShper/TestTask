using System.Drawing;

namespace TestTask.Core;

public class CustomPolygon : PointPolygonBase
{
    public required List<Coordinates> Points { get; set; }
}