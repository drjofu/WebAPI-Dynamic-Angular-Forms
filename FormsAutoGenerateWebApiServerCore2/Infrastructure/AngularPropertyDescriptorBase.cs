namespace FormsAutoGenerateAnsatzWebApiServer.Infrastructure
{
  public class AngularPropertyDescriptorBase
  {
    // Feldname
    public string Name { get; set; }

    // Anzeigename für Tabelle / Editor
    public string Label { get; set; }

    // Reihenfolge
    public int Order { get; set; }

    // Umschaltung <input> / <select>
    public string ControlType { get; set; }

    // Typ für <input>-Control
    public string DisplayType { get; set; }

    // Validierungen
    public bool Required { get; set; }
    public int? MinLength { get; set; }
    public int? MaxLength { get; set; }

    public object Minimum { get; set; }
    public object Maximum { get; set; }

    // Darstellungsoptionen
    public bool Visible { get; internal set; } = true;
    public bool ReadOnly { get; internal set; }

    // Feld ist Primärschlüssel
    public bool IsPrimaryKey { get; set; }

  }

}
