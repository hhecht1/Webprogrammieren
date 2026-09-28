
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechCamp.Data;
using TechCamp.Models;
using TechCamp.ViewModels;
namespace TechCamp.Controllers;


public class TeilnehmerController : Controller
{
    private readonly TechCampContext _db;

    private readonly ILogger<TeilnehmerController> _logger;
    public TeilnehmerController(TechCampContext db, ILogger<TeilnehmerController> logger)
    {
        _db = db;
        _logger = logger;
    }






}