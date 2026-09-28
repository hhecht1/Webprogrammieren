using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechCamp.Data;
using TechCamp.Models;

namespace TechCamp.Controllers;

public class RäumeController : Controller
{
    private readonly TechCampContext _db;

    public RäumeController(TechCampContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var raeume = await _db.Räume
            .OrderBy(r => r.Gebäude)
            .ThenBy(r => r.Bezeichnung)
            .ToListAsync();

        return View(raeume);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Raum raum)
    {
        if (!ModelState.IsValid)
        {
            return View(raum);
        }

        _db.Räume.Add(raum);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}