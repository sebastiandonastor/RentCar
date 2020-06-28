using RentCar.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.DAL.EntitiesConfiguration
{
    public class ClienteConfiguration : EntityTypeConfiguration<Cliente>
    {
        public ClienteConfiguration()
        {
            this.ToTable("Clientes");
            this.HasKey(c => c.Id);

            this.HasMany(c => c.Rentas)
              .WithRequired(r => r.Cliente)
              .HasForeignKey(r => r.IdCliente);

            this.HasMany(c => c.Inspecciones)
                .WithRequired(i => i.Cliente)
                .HasForeignKey(i => i.IdCliente);
        }
    }
}
