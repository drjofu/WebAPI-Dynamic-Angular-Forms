using FormsAutoGenerateAnsatzWebApiServer.Reflection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormsAutoGenerateWebApiServerCore2.Models
{
  public class Kunde
  {
    [ReadOnly(true)]
    public int Id { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(15)]
    [Display(Name="Vor- und Zuname")]
    public string Name { get; set; }

    [Display(Name="Genaue Adresse")]
    public string Adresse { get; set; }

    [Range(18,120)]
    [DividableBy(5)]
    [EvenNumber]
    [DataType("number")]
    public int Alter { get; set; }
  }
}
