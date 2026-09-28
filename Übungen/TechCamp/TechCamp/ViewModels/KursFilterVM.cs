using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechCamp.Models;

namespace TechCamp.ViewModels;

public class KursFilterVM
{
    public string? Titelsuche { get; set; }
    public Kursart? Kursart { get; set; }
    public DateOnly? VonDatum { get; set; }
    public DateOnly? BisDatum { get; set; }
    public bool NurPlätzeFrei { get; set; }
    public int RaumId { get; set; }

    public IEnumerable<SelectListItem> RaumeListe { get; set; } = Enumerable.Empty<SelectListItem>();
}