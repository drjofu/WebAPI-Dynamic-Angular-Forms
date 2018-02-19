using Microsoft.EntityFrameworkCore;

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
