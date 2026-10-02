namespace WerkstattPro.Models;

public class  ReparaturAuftrag
{
    public int Id { get; set; }
    public string Beschreibung { get; set; } = string.Empty;
    public DateOnly AuftragsDatum { get; set; }
    public bool Status { get; set; }
    public int FahrzeugId { get; set; }
    public Fahrzeug Fahrzeug { get; set; } = null!;
    public ICollection<AuftragMechaniker> AuftragMechaniker { get; set; } = new List<AuftragMechaniker>();
    public ICollection<AuftragErsatzteil> AuftragErsatzteil { get; set; } = new List<AuftragErsatzteil>();
}

