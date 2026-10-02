namespace WerkstattPro.Models;

    public class Fahrzeug
    {
        public int Id { get; set; }
        public string? Kennzeichen { get; set; }
        public string? Marke { get; set; }
        public string? Model { get; set; }
        public DateTime Baujahr { get; set; }
        public int KundeId { get; set; }
        public Kunde Kunde { get; set; } = null!;
    public ICollection<ReparaturAuftrag> ReparaturAufträge { get; set; } = new List<ReparaturAuftrag>();
}



