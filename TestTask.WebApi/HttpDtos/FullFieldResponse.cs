namespace TestTask.Logic.DTO;

public class FullFieldResponse 
{
    public required int ID { get; set; }
    public required string Name { get; set; }
    public required float Size { get; set; }
    public required FullLocationData Locations { get; set; }
}