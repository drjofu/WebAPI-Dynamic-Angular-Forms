using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormsAutoGenerateAnsatzWebApiServer.Reflection
{
  [AttributeUsage(AttributeTargets.Property)]
  public class LookupTableAttribute : Attribute
  {
    // Url zum Abruf der Daten
    public string LookupUrl { get; set; }

    // Schlüsselfeld
    public string KeyField { get; set; }

    // Feld mit den anzuzeigenden Informationen
    public string ValueField { get; set; }
  }
}
