using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechCamp.Models;

namespace TechCamp.ViewModels;

public class KursFilterVM
{
    public string? Titelsuche { get; set; }
    public Kursart? Kursart { get; set; }
    public DateOnly? Vondatum { get; set; }
    public DateOnly? Bisdatum { get; set; }
    public bool? NurPlätzeFrei { get; set; }
    public int RaumId { get; set; }


}