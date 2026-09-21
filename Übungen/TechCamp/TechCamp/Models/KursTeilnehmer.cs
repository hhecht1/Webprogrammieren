namespace TechCamp.Models;

public class KursTeilnehmer
{
    public int KursId { get; set; }
    public int TeilnehmerId { get; set; }
    public Status Status { get; set; } = null!;
    public Kurs Kurs { get; set; } = null!;
    public Teilnehmer Teilnehmer { get; set; } = null!;
    public DateTime Anmeldedatum { get; set; }
}