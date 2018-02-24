using FormsAutoGenerateAnsatzWebApiServer.Reflection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormsAutoGenerateWebApiServerCore2.Models
{
  public class Flugzeug
  {
    public int Id { get; set; }
    public string Typbezeichnung { get; set; }

    [Range(200,30000)]
    [Display(Name ="Reichweite in km")]
    [DataType("number")]
    [DividableBy(100)]
    public int Reichweite { get; set; }
  }
}
