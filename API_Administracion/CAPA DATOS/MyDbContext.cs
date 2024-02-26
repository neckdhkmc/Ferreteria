using API_Administracion.Modelos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace API_Administracion.CAPA_DATOS
{
     
    public class MyDbContext : DbContext
    {

        
        public MyDbContext() : base(GetConnectionString()) // El nombre debe coincidir con el nombre de la cadena de conexión en tu archivo de configuración
        {

        }
        public DbSet<Provedores> Provedores { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<ProvedorMarca> ProvedorMarca { get; set; }

        //se obtiene la cadena de conexion
        private static string GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            return configuration.GetConnectionString("MiConexionBD");
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProvedorMarca>()
                .HasKey(pm => pm.IdProvedor)
                .Property(pm => pm.IdProvedor)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); // Esto indica que la clave es generada automáticamente por la base de datos

            // Otros configuraciones...
        }

    }
}
