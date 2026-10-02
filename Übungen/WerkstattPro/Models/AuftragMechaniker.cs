namespace WerkstattPro.Models;

public class AuftragMechaniker
{
    public int ReparaturAuftragId { get; set; }
    public int MechanikerId { get; set; }
    public ReparaturAuftrag? ReparaturAuftrag { get; set; }
    public Mechaniker? Mechaniker { get; set; }


}