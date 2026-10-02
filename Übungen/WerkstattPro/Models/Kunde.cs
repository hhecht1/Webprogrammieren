namespace WerkstattPro.Models;

    public class Kunde
    {
        public int Id { get; set; }
        public string? Vorname { get; set; }
        public string? Nachname { get; set; }
        public string? Email { get; set; }
        public int Telefonnummer { get; set; }
        public ICollection<Fahrzeug> Fahrzeuge { get; set; } = new List<Fahrzeug>();
    }
