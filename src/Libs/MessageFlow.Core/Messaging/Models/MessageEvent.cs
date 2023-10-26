namespace MessageFlow.Core.Messaging.Models;

public class MessageEvent
{
       public string AanvragerKey { get; set; }  = string.Empty;
       public string BerichtType { get; set; }  = string.Empty;
       public string DatumDagtekening { get; set; }  = string.Empty;
       public string DatumVraagbrief { get; set; }  = string.Empty;
       public bool KgbVariant { get; set; }  = false;
       public string Reactiedatum { get; set; }  = string.Empty;
       public string Toeslagjaar { get; set; }  = string.Empty;
}