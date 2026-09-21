using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechCamp.Data;
using TechCamp.Models;
using TechCamp.ViewModels;
using System.Reflection;


namespace TechCamp.Controllers;

public class KurseController : Controller
{

    private readonly TechCampContext _db;
    private readonly ILogger<KurseController> _logger;

    public KurseController(TechCampContext db, ILogger<KurseController> logger)
    {
        _db = db;
        _logger = logger;
    }
    public async Task<IActionResult> Index()
    {
        var kurse = await _db.Kurse
        .Include(k => k.Raum)
        .Include(k => k.KursTeilnehmer)
        .OrderBy(k => k.StartDatum)
        .ToListAsync();

        return View(kurse);
    }

    public async Task<IActionResult> Details(int Id)

    {
        var kurs = await _db.Kurse
        .Include(k => k.Raum)
        .Include(k => k.KursDozenten)
            .ThenInclude(kd => kd.Dozent)
        .Include(k => k.KursTeilnehmer)
            .ThenInclude(kt => kt.Teilnehmer)
        .FirstOrDefaultAsync(k => k.Id == Id);

        if (kurs == null)
            return NotFound();
        return View(kurs);
    }

    public IActionResult Create()
    {
        var vm = new KursCreateVM();
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(KursCreateVM vM)
    {
        if (!ModelState.IsValid)
        {
            await Task.CompletedTask;
            return View(vM);
        }
        var kurs = new Kurs
        {
            Titel = vM.Titel,
            Beschreibung = vM.Beschreibung,
            Kursart = vM.Kursart,
            MaxTeilnehmer = vM.MaxTeilnehmer,
            Wiederholung = vM.Wiederholung,
            StartDatum = vM.StartDatum,
            EndDatum = vM.EndDatum,
            RaumId = vM.RaumId
        };
        _db.Kurse.Add(kurs);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int Id)
    {
        var kurs = await _db.Kurse
        .Include(k => k.Dozent)
        .FirstOrDefaultAsync(k => k.Id == Id);
        if (kurs == null)
            return NotFound();


        var vM = new KursEditVM
        {
            Titel = kurs.Titel,
            Beschreibung = kurs.Beschreibung,
            Kursart = kurs.Kursart,
            MaxTeilnehmer = kurs.MaxTeilnehmer,
            Wiederholung = kurs.Wiederholung,
            StartDatum = kurs.StartDatum,
            EndDatum = kurs.EndDatum,
            RaumId = kurs.RaumId,

            DozentId = kurs.KursDozenten
            .Select(kd => kd.DozentId)
            .ToList()
        };
        return View(vM);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int Id, KursEditVM vM)
    {
        if (!ModelState.IsValid)
        {
            await Task.CompletedTask;
            return View(vM);
        }
        var kurs = await _db.Kurse
        .Include(k => k.KursDozenten)
        .FirstOrDefaultAsync(k => k.Id == Id);

        if (kurs == null)
            return NotFound();

        kurs.Titel = vM.Titel;
        kurs.Beschreibung = vM.Beschreibung;
        kurs.Kursart = vM.Kursart;
        kurs.MaxTeilnehmer = vM.MaxTeilnehmer;
        kurs.Wiederholung = vM.Wiederholung;
        kurs.StartDatum = vM.StartDatum;
        kurs.EndDatum = vM.EndDatum;
        kurs.RaumId = vM.RaumId;

        kurs.KursDozenten.Clear();
        foreach (var dozentId in vM.DozentId)
        {
            kurs.KursDozenten.Add(new KursDozent
            {
                KursId = kurs.Id,
                DozentId = dozentId
            });
        }

        _db.Kurse.Update(kurs);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int Id)
    {
        var kurs = await _db.Kurse.FindAsync(Id);
        if (kurs == null)
            return NotFound();

        bool hatAngemeldeteTeilnehmer = kurs.Teilnehmer.Any();
        if (hatAngemeldeteTeilnehmer)
            return BadRequest("Der Kurs hat angemeldete Teilnehmer und kann nicht gelöscht werden.");


        _db.Kurse.Remove(kurs);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


}