using FormsAutoGenerateWebApiServerCore2.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FormsAutoGenerateWebApiServerCore2.Controllers
{
  [Produces("application/json")]
  [Route("api/Metadata")]
  public class MetadataController : Controller
  {
    private readonly IEnumerable<Tabledefinition> tabledefinitions;

    public MetadataController(IEnumerable<Tabledefinition> tabledefinitions)
    {
      this.tabledefinitions = tabledefinitions;
    }


    [HttpGet("tabledefinitions")]
    public IEnumerable<Tabledefinition> Get()
    {
      return tabledefinitions;
      
    }
  }
}


// alte Implementierungen
//[HttpGet("{typename}")]
//public IEnumerable<AngularPropertyDescriptorBase> Get(string typename)
//{
//  typename = typename.ToPascalCase();
//  Type type = Assembly.GetExecutingAssembly().ExportedTypes.FirstOrDefault(t => t.Name == typename);

//  if (type == null) throw new InvalidOperationException(typename + " not found");
//  var props = AngularPropertyDescriptionGenerator.GetAngularPropertyDescriptionForType(type).OrderBy(p => p.Order);

//  return props;
//}

//return new Tabledefinition[] {
//  new Tabledefinition { Name = "Artikel", Typename = nameof(Artikel), Url = "Artikel" } ,
//  new Tabledefinition{Name="Kategorie",Typename=nameof(Artikelkategorie), Url="Artikelkategorien" }
//};
