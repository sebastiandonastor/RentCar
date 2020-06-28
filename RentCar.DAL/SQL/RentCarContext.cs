using RentCar.DAL.EntitiesConfiguration;
using RentCar.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.DAL.SQL
{
    public class RentCarContext : DbContext
    {
        public RentCarContext() : base("RentCarDb")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new TipoPersonaConfiguration());
            modelBuilder.Configurations.Add(new EmpleadosConfiguration());
            modelBuilder.Configurations.Add(new InspeccionConfiguration());
            modelBuilder.Configurations.Add(new MarcaConfiguration());
            modelBuilder.Configurations.Add(new ModeloConfiguration());
            modelBuilder.Configurations.Add(new RentaConfiguration());
            modelBuilder.Configurations.Add(new TipoCombustibleConfiguration());
            modelBuilder.Configurations.Add(new TipoVehiculoConfiguration());
            modelBuilder.Configurations.Add(new VehiculoConfiguration());
            modelBuilder.Configurations.Add(new ClienteConfiguration());


        }
        public DbSet<TipoVehiculo> TipoVehiculos { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Renta> Rentas { get; set; }
        public DbSet<Inspeccion> Inspecciones { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<TipoPersona> TiposPersonas { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<TipoCombustible> TiposCombustibles { get; set; }

    }
}
