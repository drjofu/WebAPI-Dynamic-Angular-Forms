using FormsAutoGenerateWebApiServerCore2.Infrastructure;
using FormsAutoGenerateWebApiServerCore2.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace FormsAutoGenerateWebApiServerCore2
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      // Demo-Datenbank initialisieren
      DatenInit.Init();

      services.AddCors();

      services.AddMvc();

      // DbContext über DI verfügbar machen
      services.AddDbContext<WarenhausContext>();

      // Metadaten als Singleton bereitstellen
      services.AddSingleton<IEnumerable<Tabledefinition>>(Tabledefinition.CreateFromDbContext<WarenhausContext>());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
      app.UseMvc();
    }
  }
}
