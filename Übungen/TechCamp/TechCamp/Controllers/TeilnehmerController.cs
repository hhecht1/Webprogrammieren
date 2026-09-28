
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechCamp.Data;
using TechCamp.Models;
using TechCamp.ViewModels;
namespace TechCamp.Controllers;

using Microsoft.AspNetCore.Mvc.Rendering;


public class TeilnehmerController : Controller
{
    private readonly TechCampContext _db;

    private readonly ILogger<TeilnehmerController> _logger;
    public TeilnehmerController(TechCampContext db, ILogger<TeilnehmerController> logger)
    {
        _db = db;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var teilnehmer = await _db.Teilnehmer
        .OrderBy(t => t.Nachname)
        .ThenBy(t => t.Vorname)
        .ToListAsync();

        return View(teilnehmer);
    }

    [HttpGet]

    public async Task<IActionResult> Details(int id)
    {
        var teilnehmer = await _db.Teilnehmer
        .Include(t => t.KursTeilnehmer)
        .ThenInclude(kt => kt.Kurs)
        .FirstOrDefaultAsync(t => t.Id == id);

        if (teilnehmer == null)
        {
            return NotFound();
        }

        return View(teilnehmer);
    }

    [HttpGet]

    public async Task<IActionResult> Anmelden(int kursId)
    {
        var kurs = await _db.Kurse.FindAsync(kursId);
        if (kurs == null) return NotFound();

        var vm = new TeilnehmerAnmeldungVM
        {
            TeilnehmerListe = new SelectList(
                await _db.Teilnehmer.OrderBy(t => t.Nachname).ToListAsync(),
                "Id", "Nachname")
        };

        ViewBag.KursId = kursId;
        ViewBag.KursTitel = kurs.Titel;
        return View(vm);
    }

    [HttpPost]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Anmelden(int kursId, TeilnehmerAnmeldungVM vm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.KursId = kursId;
            return View(vm);
        }

        var kurs = await _db.Kurse
            .Include(k => k.KursTeilnehmer)
            .FirstOrDefaultAsync(k => k.Id == kursId);

        if (kurs == null) return NotFound();

        // Kapazitätsprüfung
        var angemeldet = kurs.KursTeilnehmer.Count(kt => kt.Status == Status.Angemeldet);
        if (angemeldet >= kurs.MaxTeilnehmer)
        {
            _logger.LogWarning("Anmeldeversuch für Kurs \"{KursTitel}\" fehlgeschlagen – keine Plätze frei.", kurs.Titel);
            TempData["Error"] = $"Kurs \"{kurs.Titel}\" ist voll.";
            return RedirectToAction(nameof(Index));
        }

        // Doppelte Anmeldung prüfen
        if (kurs.KursTeilnehmer.Any(kt => kt.TeilnehmerId == vm.TeilnehmerId))
        {
            TempData["Error"] = "Teilnehmer ist bereits für diesen Kurs angemeldet.";
            return RedirectToAction(nameof(Index));
        }

        var teilnehmer = await _db.Teilnehmer.FindAsync(vm.TeilnehmerId);
        if (teilnehmer == null) return NotFound();

        kurs.KursTeilnehmer.Add(new KursTeilnehmer
        {
            TeilnehmerId = vm.TeilnehmerId,
            Status = vm.Status,
            AnmeldeDatum = DateTime.Today
        });

        await _db.SaveChangesAsync();

        _logger.LogInformation("Teilnehmer {Name} (Id: {Id}) für Kurs \"{KursTitel}\" angemeldet.",
            teilnehmer.Nachname, teilnehmer.Id, kurs.Titel);

        return RedirectToAction(nameof(Index));
    }

    // POST: Teilnehmer/StatusAendern
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StatusAendern(int kursId, int teilnehmerId, Status neuerStatus)
    {
        var kt = await _db.KursTeilnehmer
            .FirstOrDefaultAsync(x => x.KursId == kursId && x.TeilnehmerId == teilnehmerId);

        if (kt == null) return NotFound();

        kt.Status = neuerStatus;
        await _db.SaveChangesAsync();

        _logger.LogInformation("Status für Teilnehmer {TeilnehmerId} in Kurs {KursId} geändert zu {Status}.",
            teilnehmerId, kursId, neuerStatus);

        return RedirectToAction(nameof(Index));
    }

    // POST: Teilnehmer/Abmelden
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Abmelden(int kursId, int teilnehmerId)
    {
        var kt = await _db.KursTeilnehmer
            .FirstOrDefaultAsync(x => x.KursId == kursId && x.TeilnehmerId == teilnehmerId);

        if (kt == null) return NotFound();

        _db.KursTeilnehmer.Remove(kt);
        await _db.SaveChangesAsync();

        _logger.LogInformation("Teilnehmer {TeilnehmerId} von Kurs {KursId} abgemeldet.",
            teilnehmerId, kursId);

        return RedirectToAction(nameof(Index));
    }









}