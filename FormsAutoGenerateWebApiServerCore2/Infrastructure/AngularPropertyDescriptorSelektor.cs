namespace FormsAutoGenerateAnsatzWebApiServer.Infrastructure
{
  public class AngularPropertyDescriptorSelektor : AngularPropertyDescriptorBase
  {
    // Url zum Abruf der Daten
    public string LookupUrl { get; set; }

    // Schlüsselfeld
    public string LookupIdField { get; set; }

    // Feld mit den anzuzeigenden Informationen
    public string LookupValueField { get; set; }

    public AngularPropertyDescriptorSelektor()
    {
      this.ControlType = "dropdown"; 
    }
  }
}
