using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechCamp.Data;
using TechCamp.Models;
using TechCamp.ViewModels;
using System.Linq;
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

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var kurse = await _db.Kurse
        .Include(k => k.Raum)
        .Include(k => k.KursTeilnehmer)
        .OrderBy(k => k.StartDatum)
        .ToListAsync();

        return View(kurse);
    }

    [HttpGet]
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

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var vm = new KursCreateVM
        {
            RaumeListe = new SelectList(
                await _db.Räume
                    .OrderBy(r => r.Bezeichnung)
                    .ToListAsync(),
                "Id",
                "Bezeichnung"),

            AlleDozenten = await _db.Dozenten
                .OrderBy(d => d.Nachname)
                .ToListAsync()
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(KursCreateVM vM)
    {
        if (!ModelState.IsValid)
        {
            vM.RaumeListe = new SelectList(
                await _db.Räume
                    .OrderBy(r => r.Bezeichnung)
                    .ToListAsync(),
                "Id",
                "Bezeichnung",
                vM.RaumId);

            vM.AlleDozenten = await _db.Dozenten
                .OrderBy(d => d.Nachname)
                .ToListAsync();

            return View(vM);
        }

        var kurs = new Kurs
        {
            Titel = vM.Titel,
            Beschreibung = vM.Beschreibung,
            Kursart = vM.Kursart,
            MaxTeilnehmer = vM.MaxTeilnehmer,
            Wiederholung = vM.Wiederholung,
            StartDatum = vM.StartDatum.ToDateTime(TimeOnly.MinValue),
            EndDatum = vM.EndDatum.ToDateTime(TimeOnly.MinValue),
            RaumId = vM.RaumId
        };

        foreach (var dozentId in vM.DozentIds)
        {
            kurs.KursDozenten.Add(new KursDozent
            {
                DozentId = dozentId
            });
        }

        _db.Kurse.Add(kurs);
        await _db.SaveChangesAsync();

        _logger.LogInformation(
            "Kurs {KursId} ({Titel}) wurde erstellt.",
            kurs.Id,
            kurs.Titel);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int Id)
    {
        var kurs = await _db.Kurse
            .Include(k => k.KursDozenten)
            .FirstOrDefaultAsync(k => k.Id == Id);

        if (kurs == null)
            return NotFound();

        var raume = await _db.Räume
            .OrderBy(r => r.Bezeichnung)
            .ToListAsync();
        var dozenten = await _db.Dozenten
            .OrderBy(d => d.Nachname)
            .ToListAsync();

        var vM = new KursEditVM
        {
            Id = kurs.Id,
            Titel = kurs.Titel,
            Beschreibung = kurs.Beschreibung,
            Kursart = kurs.Kursart,
            MaxTeilnehmer = kurs.MaxTeilnehmer,
            Wiederholung = kurs.Wiederholung,
            StartDatum = DateOnly.FromDateTime(kurs.StartDatum),
            EndDatum = DateOnly.FromDateTime(kurs.EndDatum),
            RaumId = kurs.RaumId,
            DozentIds = kurs.KursDozenten
                .Select(kd => kd.DozentId)
                .ToList(),
            RaumeListe = new SelectList(
                await _db.Räume
                    .OrderBy(r => r.Bezeichnung)
                    .ToListAsync(),
                "Id",
                "Bezeichnung",
                kurs.RaumId),
            AlleDozenten = await _db.Dozenten
                .OrderBy(d => d.Nachname)
                .ToListAsync()
        };

        return View(vM);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int Id, KursEditVM vM)
    {
        if (!ModelState.IsValid)
        {
            vM.RaumeListe = new SelectList(
           await _db.Räume
               .OrderBy(r => r.Bezeichnung)
               .ToListAsync(),
           "Id",
           "Bezeichnung",
           vM.RaumId);

            vM.AlleDozenten = await _db.Dozenten
                .OrderBy(d => d.Nachname)
                .ToListAsync();

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
        kurs.StartDatum = vM.StartDatum.ToDateTime(TimeOnly.MinValue);
        kurs.EndDatum = vM.EndDatum.ToDateTime(TimeOnly.MinValue);
        kurs.RaumId = vM.RaumId;

        _db.KursDozenten.RemoveRange(kurs.KursDozenten);

        foreach (var dozentId in vM.DozentIds)
        {
            kurs.KursDozenten.Add(new KursDozent
            {
                KursId = kurs.Id,
                DozentId = dozentId
            });
        }

        _db.Kurse.Update(kurs);
        await _db.SaveChangesAsync();

        _logger.LogInformation("Kurs mit Id {KursId} wurde bearbeitet.", kurs.Id);
        return RedirectToAction(nameof(Index));


    }

    public async Task<IActionResult> Delete(int Id)
    {
        var kurs = await _db.Kurse.FindAsync(Id);
        if (kurs == null)
            return NotFound();

        bool hatAngemeldeteTeilnehmer = kurs.KursTeilnehmer.Any();
        if (hatAngemeldeteTeilnehmer)
            return BadRequest("Der Kurs hat angemeldete Teilnehmer und kann nicht gelöscht werden.");


        _db.Kurse.Remove(kurs);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Suche(KursFilterVM filter)
    {
        var query = _db.Kurse
        .Include(k => k.Raum)
        .Include(k => k.KursDozenten)
            .ThenInclude(kd => kd.Dozent)
        .Include(k => k.KursTeilnehmer)
            .ThenInclude(kt => kt.Teilnehmer)
        .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Titelsuche))
            query = query.Where(k => k.Titel.Contains(filter.Titelsuche));

        if (filter.Kursart.HasValue)
            query = query.Where(k => k.Kursart == filter.Kursart.Value);

        if (filter.VonDatum.HasValue)
            query = query.Where(k => k.StartDatum >= filter.VonDatum.Value.ToDateTime(TimeOnly.MinValue));

        if (filter.BisDatum.HasValue)
            query = query.Where(k => k.StartDatum <= filter.BisDatum.Value.ToDateTime(TimeOnly.MinValue));

        if (filter.RaumId > 0)
            query = query.Where(k => k.RaumId == filter.RaumId);

        if (filter.NurPlätzeFrei == true)
            query = query.Where(k => k.KursTeilnehmer.Count(kt => kt.Status == Status.Angemeldet) < k.MaxTeilnehmer);

        var kurse = await query.OrderBy(k => k.StartDatum).ToListAsync();
        return View(kurse);
    }

    [HttpGet]
    public async Task<IActionResult> DozentenÜbersicht()
    {
        var daten = await _db.Dozenten
        .LeftJoin(
            _db.KursDozenten,
            d => d.Id,
            kd => kd.DozentId,
            (d, kd) => new { Dozent = d, KursDozenten = kd }
        )
      .GroupBy(x => new
      {
          x.Dozent.Id,
          x.Dozent.Vorname,
          x.Dozent.Nachname,
          x.Dozent.Fachgebiet,
      })
      .Select(g => new DozentÜbersichtVM
      {
          Vorname = g.Key.Vorname,
          Nachname = g.Key.Nachname,
          Fachgebiet = g.Key.Fachgebiet,
          KursAnzahl = g.Count(x => x.KursDozenten != null)
      })
        .OrderBy(d => d.Nachname)
        .ToListAsync();
        return View(daten);
    }
}