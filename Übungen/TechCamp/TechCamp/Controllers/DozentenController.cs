using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechCamp.Data;
using TechCamp.Models;
using TechCamp.ViewModels;

namespace TechCamp.Controllers;

public class DozentenController : Controller
{
    private readonly TechCampContext _db;

    public DozentenController(TechCampContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var daten = await _db.Dozenten
            .LeftJoin(
                _db.KursDozenten,
                d => d.Id,
                kd => kd.DozentId,
                (d, kd) => new
                {
                    Dozent = d,
                    KursDozent = kd
                })
            .GroupBy(x => new
            {
                x.Dozent.Id,
                x.Dozent.Vorname,
                x.Dozent.Nachname,
                x.Dozent.Fachgebiet
            })
            .Select(g => new DozentÜbersichtVM
            {
                Vorname = g.Key.Vorname,
                Nachname = g.Key.Nachname,
                Fachgebiet = g.Key.Fachgebiet,
                KursAnzahl = g.Count(x => x.KursDozent != null)
            })
            .OrderBy(d => d.Nachname)
            .ToListAsync();

        return View(daten);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Dozent dozent)
    {
        if (!ModelState.IsValid)
        {
            return View(dozent);
        }

        _db.Dozenten.Add(dozent);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}