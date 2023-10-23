namespace MessageFlow.Core;

public class Context
{
    public string Type { get; set; }
    public int Toeslagjaar { get; set; }
    public DateTime Datumdagtekening { get; set; }
    public Guid AanvragerId { get; set; }
    public Guid SubjectId { get; set; }
}