using RentCar.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.DAL.EntitiesConfiguration
{
    public class MarcaConfiguration : EntityTypeConfiguration<Marca>
    {
        public MarcaConfiguration()
        {
            this.ToTable("Marcas");
            this.HasKey(m => m.Id);

            this.HasMany(m => m.Modelos)
                .WithRequired(m => m.Marca)
                .HasForeignKey(m => m.IdMarca);

            this.HasMany(m => m.Vehiculos)
                .WithRequired(v => v.Marca)
                .HasForeignKey(v => v.IdMarca);


        }
    }
}
