namespace WerkstattPro.Models
{
    public class AuftragErsatzteil
    {
        public int ReperaturAuftragId { get; set; }
        public int ErsatzteilId { get; set; }
        public int Menge { get; set; }
        public decimal Einzlpreis { get; set; }

    }
}