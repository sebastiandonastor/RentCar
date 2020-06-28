using RentCar.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.DAL.EntitiesConfiguration
{
    public class RentaConfiguration : EntityTypeConfiguration<Renta>
    {
        public RentaConfiguration()
        {
            this.ToTable("Rentas");
            this.HasKey(r => r.Id);
        }
    }
}
