namespace TechCamp.Models;

public class Teilnehmer
{
    public int Id { get; set; }
    public string Vorname { get; set; } = string.Empty;
    public string Nachname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public ICollection<KursTeilnehmer> KursTeilnehmer { get; set; } = new List<KursTeilnehmer>();
}