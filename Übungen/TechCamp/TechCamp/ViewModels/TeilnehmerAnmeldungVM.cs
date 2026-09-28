using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TechCamp.Models;

namespace TechCamp.ViewModels;



public class TeilnehmerAnmeldungVM
{
    [Required]
    public int TeilnehmerId { get; set; }

    public Status Status { get; set; } = Status.Angemeldet;

    public SelectList? TeilnehmerListe { get; set; }

    public int KursId { get; set; }
}