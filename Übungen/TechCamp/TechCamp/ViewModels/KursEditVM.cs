using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TechCamp.Models;

namespace TechCamp.ViewModels;

public class KursEditVM
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Titel { get; set; } = string.Empty;

    public string? Beschreibung { get; set; }

    [Required]
    public Kursart Kursart { get; set; }

    [Required]

    public DateOnly StartDatum { get; set; }

    [Required]

    public DateOnly EndDatum { get; set; }

    [Range(1, 100)]
    public int MaxTeilnehmer { get; set; }

    public bool Wiederholung { get; set; }

    [Required]
    public int RaumId { get; set; }

    // Ausgewählte Dozenten
    public List<int> DozentIds { get; set; } = new();

    // Daten für die Dropdowns / Checkboxen
    public SelectList RaumeListe { get; set; } = null!;

    public List<Dozent> AlleDozenten { get; set; } = new();

    public List<Kursart> Kursarten { get; set; }
        = Enum.GetValues<Kursart>().ToList();
}