using FormsAutoGenerateAnsatzWebApiServer.Infrastructure;
using FormsAutoGenerateWebApiServerCore2.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FormsAutoGenerateWebApiServerCore2.Infrastructure
{
  public class Tabledefinition
  {
    // Anzeigename
    public string Name { get; set; }

    // Web-API-Url (relativ zur Basisadresse)
    public string Url { get; set; }

    // Typ der Entity-Klasse
    public string Typename { get; set; }

    // Primärschlüsselfeld
    public string PrimaryKey { get; set; }

    // Property-Metadaten für die anzuzeigenden Eigenschaften
    public IEnumerable<AngularPropertyDescriptorBase> PropertyDescriptors { get; set; }

    // Automatische Generierung aus DbContext-Klasse
    public static IEnumerable<Tabledefinition> CreateFromDbContext<TDbContext>() where TDbContext : DbContext, new()
    {
      using (var ctx = new TDbContext())
      {

        var type = typeof(TDbContext);

        // Alle Property der Kontextklasse, die vom Typ DbSet<> sind
        var dbsets = type.GetProperties()
          .Where(t => t.PropertyType.IsGenericType 
          && t.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

        var tds = new List<Tabledefinition>();

        // Alle DbSets bearbeiten
        foreach (var p in dbsets)
        {
          // Typ der Model-Klasse
          var pt = p.PropertyType.GenericTypeArguments[0];

          // Metadaten für Model-Klasse anlegen
          var td = new Tabledefinition
          {
            Name = p.Name,
            Typename = p.PropertyType.GenericTypeArguments[0].Name,
            Url = p.Name,
            PropertyDescriptors = AngularPropertyDescriptionGenerator
              .GetAngularPropertyDescriptionForType(pt, ctx)
              .OrderBy(pd=>pd.Order)
              .ToList()
          };

          // Primärschlüssel ermitteln
          var et = ctx.Model.FindEntityType(pt);
          var pk = et.FindPrimaryKey();
          td.PrimaryKey = pk?.Properties.FirstOrDefault()?.Name.ToCamelCase();

          tds.Add(td);
        }
        return tds;
      }
    }
  }
}
