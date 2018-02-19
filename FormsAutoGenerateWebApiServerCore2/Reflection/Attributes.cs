using System;
using System.ComponentModel.DataAnnotations;

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

  //[AttributeUsage(AttributeTargets.Property)]
  //public class EvenNumberAttribute : ValidationAttribute
  //{
  //  public override bool IsValid(object value)
  //  {
  //    if (!(value is int)) return false;
  //    int v = (int)value;
  //    return (v % 2) == 0;
  //  }
  //}

  //[AttributeUsage(AttributeTargets.Property)]
  //public class DividableByAttribute : ValidationAttribute
  //{
  //  public int Divisor { get; set; }
  //  public DividableByAttribute(int divisor)
  //  {
  //    this.Divisor = divisor;
  //  }

  //  public DividableByAttribute() { }

  //  public override bool IsValid(object value)
  //  {
  //    if (!(value is int)) return false;
  //    int v = (int)value;
  //    return (v % Divisor) == 0;
  //  }
  //}

}
