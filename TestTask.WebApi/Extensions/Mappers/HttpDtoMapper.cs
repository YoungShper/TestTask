using TestTask.Logic.DTO;

namespace TestTask.WebApi.Extensions.Mappers;

public static class HttpDtoMapper
{
    public static FullFieldResponse ToHttpDto(this FullFieldData data) => new()
    {
        ID = data.ID,
        Locations = data.Locations,
        Size = data.Size,
        Name = data.Name,
    };

    public static FieldByPointResponse ToHttpDto(this FieldByPointData data) => new()
    {
        ID = data.ID,
        IsInField = data.IsInField,
        Name = data.Name,
    };

    public static DistanceResponse ToHttpDto(this DistanceData data) => new()
    {
        Distance = data.Distance,
    };

    public static FieldSizeResponse ToHttpDto(this FieldSizeData data) => new()
    {
        Size = data.Size,
    };
}