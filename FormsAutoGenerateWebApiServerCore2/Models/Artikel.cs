using FormsAutoGenerateAnsatzWebApiServer.Reflection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FormsAutoGenerateWebApiServerCore2.Models
{
  //[ModelMetadataType(typeof(ArtikelMetadata))]
  public class Artikel
  {
    [ScaffoldColumn(false)]
    public int Id { get; set; }

    [Display(Name = "Bezeichnung", Order = 1)]
    [Required]
    [StringLength(20)]
    public string Bezeichnung { get; set; }

    [Display(Name = "Preis", Order = 2)]
    [DisplayFormat(DataFormatString = "0.00")]
    [DataType("number")]
    public double Preis { get; set; }

    [JsonIgnore]
    public Artikelkategorie Kategorie { get; set; }

    [LookupTable(LookupUrl = "Artikelkategorien", KeyField =nameof(Artikelkategorie.Id), ValueField =nameof(Artikelkategorie.Bezeichnung))]
    [Display(Name = "Kategorie", Order = 4)]
    public int KategorieId { get; set; }

    [Display(Name = "Auf Lager", Order = 5)]
    [DataType("number")]
    public int AufLager { get; set; }

    [DataType("date")]
    [Display(Name = "Haltbar bis", Order = 6)]
    public DateTime? HaltbarBis { get; set; }

    //[DataType("checkbox")]
    //public bool Schmeckt { get; set; }

  }


 


}
