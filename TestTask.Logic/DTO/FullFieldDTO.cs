namespace TestTask.Logic.DTO;

public class FullFieldDTO 
{
    public required int ID { get; set; }
    public required string Name { get; set; }
    public required float Size { get; set; }
    public required FullLocationDTO Locations { get; set; }
}