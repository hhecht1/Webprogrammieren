namespace TechCamp.Models;

public class Raum
{
    public int Id { get; set; }
    public string Bezeichnung { get; set; } = string.Empty;
    public int Kapazität { get; set; }
    public string Gebäude { get; set; } = string.Empty;
    public ICollection<Kurs> Kurse { get; set; } = new List<Kurs>();
}