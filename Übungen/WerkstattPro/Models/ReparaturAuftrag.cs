namespace WerkstattPro.Models;

public class  ReperaturAuftrag
{
    public int Id { get; set; }
    public string Beschreibung { get; set; } = string.Empty;
    public DateTime AuftragsDatum { get; set; }
    public bool status { get; set; }
    public int FahrzeugId { get; set; }

}

