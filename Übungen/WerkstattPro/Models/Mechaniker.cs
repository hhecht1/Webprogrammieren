namespace WerkstattPro.Models;

    public class Mechaniker
    {
        public int Id { get; set; }
        public string? Vorname { get; set; }
        public string? Nachname { get; set; }
        public string? Fachgebiet { get; set; }
        public ICollection<AuftragMechaniker> AuftragMechanikers { get; set; } = new List<AuftragMechaniker>();
    }
