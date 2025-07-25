namespace TestTask.Logic.Interfaces;

public interface IFullDataServiceOptions
{
    const string FullDataService = "FullDataService";
    string FieldsPath { get; set; }
    string CentroidsPath { get; set; }
}