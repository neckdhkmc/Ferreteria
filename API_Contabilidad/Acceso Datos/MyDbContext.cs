using API_Contabilidad.Modelos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace API_Contabilidad.Acceso_Datos
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base(GetConnectionString()) // El nombre debe coincidir con el nombre de la cadena de conexión en tu archivo de configuración
        {

        }

        public DbSet<Ventas> ventas { get; set; }
        public DbSet<Productos> Productos { get; set; }
        private static string GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            return configuration.GetConnectionString("MiConexionBD");
        }

    }
}
