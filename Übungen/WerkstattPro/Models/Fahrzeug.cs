namespace WerkstattPro.Models
{
    public void class Kunde
    {
        public int Id { get; set; }
        public string? Kennzeichen { get; set; }
        public string? Marke { get; set; }
        public string? Model { get; set; }
        public DateTime Baujahr { get; set; }
        public int KundeId { get; set; }
    }
    


