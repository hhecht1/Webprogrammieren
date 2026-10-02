namespace WerkstattPro.Models;

    public class AuftragErsatzteil
    {
        public int ReparaturAuftragId { get; set; }
        public int ErsatzteilId { get; set; }
        public int Menge { get; set; }
        public decimal Einzlpreis { get; set; }
        public ReparaturAuftrag? ReparaturAuftrag { get; set; }
        public Ersatzteil? Ersatzteil { get; set; }

    }
