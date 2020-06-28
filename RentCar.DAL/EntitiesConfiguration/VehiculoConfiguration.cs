using RentCar.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.DAL.EntitiesConfiguration
{
    public class VehiculoConfiguration : EntityTypeConfiguration<Vehiculo>
    {
        public VehiculoConfiguration()
        {
            this.ToTable("Vehiculos");
            this.HasKey(v => v.Id);

            this.HasMany(v => v.Rentas)
                .WithRequired(r => r.Vehiculo)
                .HasForeignKey(r => r.IdVehiculo);

            this.HasMany(c => c.Inspecciones)
                .WithRequired(i => i.Vehiculo)
                .HasForeignKey(i => i.IdVehiculo);
        }
    }
}
