namespace WerkstattPro.Models;

public class Ersatzteil
{
    public int Id { get; set; }
    public string? Bezeichnung { get; set; }
    public int Artikelnummer { get; set; }
    public int Lagerbestand { get; set; }
    public decimal Preis { get; set; }
}   