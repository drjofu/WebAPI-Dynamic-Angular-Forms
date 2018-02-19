using FormsAutoGenerateAnsatzWebApiServer.Reflection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
    [DataType("number")]
    public int Alter { get; set; }
  }
}
