namespace TechCamp.Models;

public class Dozent
{
    public int Id { get; set; }
    public string Vorname { get; set; } = string.Empty;
    public string Nachname { get; set; } = string.Empty;
    public string Fachgebiet { get; set; } = string.Empty;
    public ICollection<KursDozent> KursDozenten { get; set; } = new List<KursDozent>();
}