using RentCar.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.DAL.EntitiesConfiguration
{
    public class TipoCombustibleConfiguration : EntityTypeConfiguration<TipoCombustible>
    {
        public TipoCombustibleConfiguration()
        {
            this.ToTable("TiposCombustibles");
            this.HasKey(t => t.Id);

            this.HasMany(t => t.Vehiculos)
            .WithRequired(v => v.TipoCombustible)
            .HasForeignKey(v => v.IdTipoCombustible);
        }
    }
}
