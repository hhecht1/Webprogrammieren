using TechCamp.Models;

namespace TechCamp.Data;

public static class SeedData
{
    public static void Seed(TechCampContext db)
    {
        if (db.Kurse.Any())
            return;

        // Räume
        var raumA101 = new Raum
        {
            Bezeichnung = "Raum A101",
            Kapazität = 25,
            Gebäude = "Hauptgebäude"
        };

        var raumB203 = new Raum
        {
            Bezeichnung = "Raum B203",
            Kapazität = 15,
            Gebäude = "Nebengebäude"
        };

        var raumC001 = new Raum
        {
            Bezeichnung = "Raum C001",
            Kapazität = 40,
            Gebäude = "Hauptgebäude"
        };

        var onlineRaum = new Raum
        {
            Bezeichnung = "Online-Room",
            Kapazität = 100
        };

        db.Räume.AddRange(
            raumA101,
            raumB203,
            raumC001,
            onlineRaum);

        // Dozenten
        var dozent1 = new Dozent
        {
            Vorname = "Anna",
            Nachname = "Steiner",
            Fachgebiet = "C# / .NET"
        };

        var dozent2 = new Dozent
        {
            Vorname = "Markus",
            Nachname = "Berger",
            Fachgebiet = "Web Development"
        };

        var dozent3 = new Dozent
        {
            Vorname = "Lisa",
            Nachname = "Winkler",
            Fachgebiet = "Datenbankdesign"
        };

        var dozent4 = new Dozent
        {
            Vorname = "Thomas",
            Nachname = "Gruber",
            Fachgebiet = "DevOps / Cloud"
        };

        db.Dozenten.AddRange(
            dozent1,
            dozent2,
            dozent3,
            dozent4);

        // Teilnehmer
        var teilnehmer1 = new Teilnehmer
        {
            Vorname = "Eva",
            Nachname = "Maier",
            Email = "eva.maier@example.at"
        };

        var teilnehmer2 = new Teilnehmer
        {
            Vorname = "Karl",
            Nachname = "Huber",
            Email = "karl.huber@example.at"
        };

        var teilnehmer3 = new Teilnehmer
        {
            Vorname = "Sofia",
            Nachname = "Novak",
            Email = "sofia.novak@example.at"
        };

        var teilnehmer4 = new Teilnehmer
        {
            Vorname = "David",
            Nachname = "Krane",
            Email = "david.krane@example.at"
        };

        var teilnehmer5 = new Teilnehmer
        {
            Vorname = "Julia",
            Nachname = "Pichler",
            Email = "julia.pichler@example.at"
        };

        var teilnehmer6 = new Teilnehmer
        {
            Vorname = "Michael",
            Nachname = "Aigner",
            Email = "michael.aigner@example.at"
        };

        db.Teilnehmer.AddRange(
            teilnehmer1,
            teilnehmer2,
            teilnehmer3,
            teilnehmer4,
            teilnehmer5,
            teilnehmer6);

        db.SaveChanges();

        // Kurse
        var kurs1 = new Kurs
        {
            Titel = "C# Grundlagen",
            Beschreibung = "Einführung in C# und .NET",
            Kursart = Kursart.Präsenz,
            StartDatum = new DateTime(2026, 10, 5),
            EndDatum = new DateTime(2026, 10, 9),
            MaxTeilnehmer = 20,
            RaumId = raumA101.Id
        };

        var kurs2 = new Kurs
        {
            Titel = "ASP.NET Core MVC",
            Beschreibung = "MVC-Pattern mit .NET 10",
            Kursart = Kursart.Präsenz,
            StartDatum = new DateTime(2026, 10, 12),
            EndDatum = new DateTime(2026, 10, 16),
            MaxTeilnehmer = 15,
            Wiederholung = true,
            RaumId = raumB203.Id
        };

        var kurs3 = new Kurs
        {
            Titel = "EF Core Deep Dive",
            Beschreibung = "Fortgeschrittene EF Core Patterns",
            Kursart = Kursart.Hybrid,
            StartDatum = new DateTime(2026, 10, 19),
            EndDatum = new DateTime(2026, 10, 23),
            MaxTeilnehmer = 20,
            RaumId = raumC001.Id
        };

        var kurs4 = new Kurs
        {
            Titel = "SQL Server Performance",
            Beschreibung = "Query Tuning und Indexierung",
            Kursart = Kursart.Online,
            StartDatum = new DateTime(2026, 11, 2),
            EndDatum = new DateTime(2026, 11, 3),
            MaxTeilnehmer = 30,
            RaumId = onlineRaum.Id
        };

        var kurs5 = new Kurs
        {
            Titel = "Docker & Kubernetes",
            Beschreibung = "Containerisierung in der Praxis",
            Kursart = Kursart.Präsenz,
            StartDatum = new DateTime(2026, 11, 9),
            EndDatum = new DateTime(2026, 11, 13),
            MaxTeilnehmer = 25,
            Wiederholung = true,
            RaumId = raumA101.Id
        };

        db.Kurse.AddRange(
            kurs1,
            kurs2,
            kurs3,
            kurs4,
            kurs5);

        db.SaveChanges();

        // KursDozenten
        db.KursDozenten.AddRange(
            new KursDozent { KursId = kurs1.Id, DozentId = dozent1.Id },
            new KursDozent { KursId = kurs2.Id, DozentId = dozent1.Id },
            new KursDozent { KursId = kurs2.Id, DozentId = dozent2.Id },
            new KursDozent { KursId = kurs3.Id, DozentId = dozent3.Id },
            new KursDozent { KursId = kurs4.Id, DozentId = dozent3.Id },
            new KursDozent { KursId = kurs4.Id, DozentId = dozent4.Id },
            new KursDozent { KursId = kurs5.Id, DozentId = dozent4.Id }
        );

        // KursTeilnehmer
        db.KursTeilnehmer.AddRange(
            new KursTeilnehmer
            {
                KursId = kurs1.Id,
                TeilnehmerId = teilnehmer1.Id,
                Status = Status.Angemeldet,
                AnmeldeDatum = new DateTime(2026, 9, 1)
            },
            new KursTeilnehmer
            {
                KursId = kurs1.Id,
                TeilnehmerId = teilnehmer2.Id,
                Status = Status.Angemeldet,
                AnmeldeDatum = new DateTime(2026, 9, 3)
            },
            new KursTeilnehmer
            {
                KursId = kurs1.Id,
                TeilnehmerId = teilnehmer3.Id,
                Status = Status.Abgesagt,
                AnmeldeDatum = new DateTime(2026, 9, 5)
            },
            new KursTeilnehmer
            {
                KursId = kurs2.Id,
                TeilnehmerId = teilnehmer1.Id,
                Status = Status.Angemeldet,
                AnmeldeDatum = new DateTime(2026, 9, 2)
            },
            new KursTeilnehmer
            {
                KursId = kurs2.Id,
                TeilnehmerId = teilnehmer4.Id,
                Status = Status.Angemeldet,
                AnmeldeDatum = new DateTime(2026, 9, 4)
            },
            new KursTeilnehmer
            {
                KursId = kurs2.Id,
                TeilnehmerId = teilnehmer5.Id,
                Status = Status.Angemeldet,
                AnmeldeDatum = new DateTime(2026, 9, 6)
            },
            new KursTeilnehmer
            {
                KursId = kurs3.Id,
                TeilnehmerId = teilnehmer6.Id,
                Status = Status.Angemeldet,
                AnmeldeDatum = new DateTime(2026, 9, 7)
            },
            new KursTeilnehmer
            {
                KursId = kurs4.Id,
                TeilnehmerId = teilnehmer2.Id,
                Status = Status.Angemeldet,
                AnmeldeDatum = new DateTime(2026, 9, 8)
            },
            new KursTeilnehmer
            {
                KursId = kurs4.Id,
                TeilnehmerId = teilnehmer3.Id,
                Status = Status.Angemeldet,
                AnmeldeDatum = new DateTime(2026, 9, 9)
            },
            new KursTeilnehmer
            {
                KursId = kurs5.Id,
                TeilnehmerId = teilnehmer5.Id,
                Status = Status.Angemeldet,
                AnmeldeDatum = new DateTime(2026, 9, 10)
            }
        );

        db.SaveChanges();
    }
}
