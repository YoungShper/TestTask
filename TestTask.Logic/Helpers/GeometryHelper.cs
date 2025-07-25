using NetTopologySuite.Geometries;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
using Coordinates = TestTask.Core.Coordinates;

namespace TestTask.Logic.Helpers;

internal static class GeometryHelper
{
    internal static double GetArea(List<Coordinates> coordinates)
    {
        var wgs84 = GeographicCoordinateSystem.WGS84;
        var utmZone37N = ProjectedCoordinateSystem.WGS84_UTM(37, true); 

        var ctFactory = new CoordinateTransformationFactory();
        var transform = ctFactory.CreateFromCoordinateSystems(wgs84, utmZone37N);

        var projectedCoordinates = coordinates
            .Select(coord =>
            {
                var projected = transform.MathTransform.Transform(new[] { coord.Lon, coord.Lat });
                return new Coordinate(projected[0], projected[1]);
            })
            .ToArray();

        var geometryFactory = new GeometryFactory();
        var polygon = geometryFactory.CreatePolygon(projectedCoordinates);

        return polygon.Area;
    }
    
    internal static double GetDistanceFromCenter(Coordinates center, double longitude, double latitude)
    {
        var wgs84 = GeographicCoordinateSystem.WGS84;
        var utmZone = ProjectedCoordinateSystem.WGS84_UTM(37, true); 

        var ctFactory = new CoordinateTransformationFactory();
        var transform = ctFactory.CreateFromCoordinateSystems(wgs84, utmZone);

        var p1 = transform.MathTransform.Transform(new double[] { center.Lon, center.Lat });
        var p2 = transform.MathTransform.Transform(new double[] { longitude, latitude });

        var geometryFactory = new GeometryFactory();
        var point1 = geometryFactory.CreatePoint(new Coordinate(p1[0], p1[1]));
        var point2 = geometryFactory.CreatePoint(new Coordinate(p2[0], p2[1]));

        return point1.Distance(point2); 
    }
    public static bool IsPointInsidePolygon(List<Coordinates> polygonCoords, double pointLat, double pointLon)
    {
        var geometryFactory = new GeometryFactory();

        var linearRing = geometryFactory.CreateLinearRing(polygonCoords.Select(x => new Coordinate(x.Lon, x.Lat)).ToArray());
        var polygon = geometryFactory.CreatePolygon(linearRing);
        
        var point = geometryFactory.CreatePoint(new Coordinate(pointLon, pointLat));

        return polygon.Contains(point);
    }
}