

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechCamp.Models;

namespace TechCamp.ViewModels;

public class DozentÜbersichtVM
{
    public string Vorname { get; set; } = string.Empty;

    public string Nachname { get; set; } = string.Empty;

    public string Fachgebiet { get; set; } = string.Empty;

    public int KursAnzahl { get; set; }
}