using RentCar.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.DAL.EntitiesConfiguration
{
    public class ModeloConfiguration : EntityTypeConfiguration<Modelo>
    {
        public ModeloConfiguration()
        {
            this.ToTable("Modelos");
            this.HasKey(m => m.Id);

            this.HasMany(m => m.Vehiculos)
                .WithRequired(v => v.Modelo)
                .HasForeignKey(v => v.IdModelo).
                WillCascadeOnDelete(false);
        }
    }
}
