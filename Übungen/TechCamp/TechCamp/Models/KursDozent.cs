namespace TechCamp.Models;

public class KursDozent
{
    public int KursId { get; set; }
    public Kurs Kurs { get; set; } = null!;
    public int DozentId { get; set; }
    public Dozent Dozent { get; set; } = null!;
}