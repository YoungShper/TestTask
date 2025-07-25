using TestTask.Logic.Interfaces;

namespace TestTask.WebApi;

public class FullDataServiceOptions : IFullDataServiceOptions
{
    public required string FieldsPath { get; set; }
    public required string CentroidsPath { get; set; }
}