using FormsAutoGenerateAnsatzWebApiServer.Reflection;
using System.ComponentModel.DataAnnotations;

namespace FormsAutoGenerateWebApiServerCore2.Models
{
  public class Bestellung
  {
    public int Id { get; set; }

    [LookupTable(LookupUrl = "Kunden", KeyField = nameof(Kunde.Id), ValueField = nameof(Kunde.Name))]
    public int KundeId { get; set; }

    [LookupTable(LookupUrl = "Artikel", KeyField = nameof(Artikel.Id), ValueField = nameof(Artikel.Bezeichnung))]
    public int ArtikelId { get; set; }

    [DividableBy(100)]
    [DataType("number")]
    public int Menge { get; set; }

  }
}
