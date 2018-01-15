using FormsAutoGenerateAnsatzWebApiServer.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormsAutoGenerateWebApiServerCore2.Models
{
  public class Bestellung
  {
    public int Id { get; set; }

    [LookupTable(LookupUrl = "Kunden", KeyField = nameof(Kunde.Id), ValueField = nameof(Kunde.Name))]
    public int KundeId { get; set; }

    [LookupTable(LookupUrl = "Artikel", KeyField = nameof(Artikel.Id), ValueField = nameof(Artikel.Bezeichnung))]
    public int ArtikelId { get; set; }

  }
}
