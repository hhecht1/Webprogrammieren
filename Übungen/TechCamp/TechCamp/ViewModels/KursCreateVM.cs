

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechCamp.Models;
namespace TechCamp.ViewModels;

public class KursCreateVM
{
    [Required, StringLength(100)]
    public string? Titel { get; set; }

    public string? Beschreibung { get; set; }

    public Kursart Kursart { get; set; }

    [Required]
    public DateOnly StartDatum { get; set; }

    [Required]
    public DateOnly EndDatum { get; set; }

    [Range(1, 100)]
    public int MaxTeilnehmer { get; set; } = 20;

    public bool Wiederholung { get; set; }

    [Required]
    public int RaumId { get; set; }

    // Multi-Select für Dozenten
    public List<int> DozentIds { get; set; } = new();

    // Für den Dropdown in der View
    public SelectList? RaumeListe { get; set; }
    public List<Dozent>? AlleDozenten { get; set; }
}