using RentCar.Entities.Models;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration;

namespace RentCar.DAL.EntitiesConfiguration
{
    public class TipoVehiculoConfiguration : EntityTypeConfiguration<TipoVehiculo>
    {
        public TipoVehiculoConfiguration()
        {
            this.ToTable("TiposVehiculos");
            this.HasKey(t => t.Id);

            this.HasMany(t => t.Vehiculos)
                .WithRequired(v => v.TipoVehiculo)
                .HasForeignKey(v => v.IdTipoVehiculo);

        }
    }
}
