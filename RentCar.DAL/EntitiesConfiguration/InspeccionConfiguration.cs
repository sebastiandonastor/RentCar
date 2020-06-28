using RentCar.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.DAL.EntitiesConfiguration
{
    public class InspeccionConfiguration : EntityTypeConfiguration<Inspeccion>
    {
        public InspeccionConfiguration()
        {
            this.ToTable("Inspecciones");
            this.HasKey(i => i.Id);
        }

    }
}
