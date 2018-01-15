using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormsAutoGenerateWebApiServerCore2.Models
{
  public class WarenhausContext : DbContext
  {
    public DbSet<Artikel> Artikel{ get; set; }
    public DbSet<Artikelkategorie> Artikelkategorien { get; set; }
    public DbSet<Kunde> Kunden { get; set; }
    public DbSet<Bestellung> Bestellungen { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseInMemoryDatabase("Demo");
    }

  }
}
