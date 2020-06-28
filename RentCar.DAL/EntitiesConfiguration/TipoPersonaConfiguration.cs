using RentCar.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.DAL.EntitiesConfiguration
{
    public class TipoPersonaConfiguration : EntityTypeConfiguration<TipoPersona>
    {
        public TipoPersonaConfiguration()
        {
            this.ToTable("TiposPersonas");

            this.HasKey(t => t.Id);

            this.HasMany(t => t.Clientes)
                .WithRequired(c => c.TipoPersona).
                HasForeignKey(c => c.IdTipoPersona);


        }
    }
}
