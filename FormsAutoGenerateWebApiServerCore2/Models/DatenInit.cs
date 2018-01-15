using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormsAutoGenerateWebApiServerCore2.Models
{
  public static class DatenInit
  {

    public static void Init()
    {
      using (var db = new WarenhausContext())
      {
        var kat1 = new Artikelkategorie { Bezeichnung = "Brotaufstrich" };
        var kat2 = new Artikelkategorie { Bezeichnung = "Eis" };
        var kat3 = new Artikelkategorie { Bezeichnung = "Schokolade" };
        var kat4 = new Artikelkategorie { Bezeichnung = "Gebäck" };
        db.Artikel.Add(new Artikel() { Id = 1042, Bezeichnung = "Orangenmarmelade", Preis = 4.56, HaltbarBis = DateTime.Today.AddDays(80), AufLager = 1, Kategorie = kat1 });
        db.Artikel.Add(new Artikel() { Id = 5545, Bezeichnung = "Schokoladeneis", Preis = 3.77, HaltbarBis = DateTime.Today.AddDays(2), AufLager = 17, Kategorie = kat2 });
        db.Artikel.Add(new Artikel() { Id = 5662, Bezeichnung = "Erdbeerschokolade", Preis = 1.77, HaltbarBis = DateTime.Today.AddDays(100), AufLager = 0, Kategorie = kat3 });
        db.Artikel.Add(new Artikel() { Id = 7373, Bezeichnung = "Mandelschokolade", Preis = 2.12, HaltbarBis = DateTime.Today.AddDays(180), AufLager = 0, Kategorie = kat3 });
        db.Artikel.Add(new Artikel() { Id = 5535, Bezeichnung = "Schokokekse", Preis = 5.55, HaltbarBis = DateTime.Today.AddDays(200), AufLager = 1, Kategorie = kat4 });

        db.Kunden.Add(new Kunde { Name = "Peter Meier", Adresse = "Köln", Alter=44 });
        db.Kunden.Add(new Kunde { Name = "Petra Müller", Adresse = "Ulm" , Alter=19});
        db.Kunden.Add(new Kunde { Name = "Uwe Schmitz", Adresse = "Köln" , Alter=77});
        db.Kunden.Add(new Kunde { Name = "Karin Matz", Adresse = "Berlin" , Alter=100});

        db.Bestellungen.Add(new Bestellung { ArtikelId = 1042, KundeId = 1 });
        db.Bestellungen.Add(new Bestellung { ArtikelId = 5662, KundeId = 2 });
        db.Bestellungen.Add(new Bestellung { ArtikelId = 5662, KundeId = 3 });
        db.Bestellungen.Add(new Bestellung { ArtikelId = 7373, KundeId = 2 });
        db.SaveChanges();
      }

    }


  }

}
