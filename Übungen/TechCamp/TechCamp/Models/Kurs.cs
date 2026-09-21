namespace TechCamp.Models;

public class Kurs
{
    public int Id { get; set; }
    public string Titel { get; set; } = string.Empty;
    public string Beschreibung { get; set; } = string.Empty;
    public Kursart Kursart { get; set; }
    public DateTime StartDatum { get; set; }
    public DateTime EndDatum { get; set; }
    public int MaxTeilnehmer { get; set; }
    public bool Wiederholung { get; set; }
    public int RaumId { get; set; }
    public Raum Raum { get; set; } = null!;
    public ICollection<KursTeilnehmer> KursTeilnehmer { get; set; } = new List<KursTeilnehmer>();
    public ICollection<KursDozent> KursDozenten { get; set; } = new List<KursDozent>();
}