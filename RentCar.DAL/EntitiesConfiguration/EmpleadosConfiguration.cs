using RentCar.Entities.Models;
using System.Data.Entity.ModelConfiguration;

namespace RentCar.DAL.EntitiesConfiguration
{
    public class EmpleadosConfiguration : EntityTypeConfiguration<Empleado>
    {
        public EmpleadosConfiguration()
        {
            this.ToTable("Empleados");
            this.HasKey(e => e.Id);

            this.HasMany(e => e.Rentas)
              .WithRequired(r => r.Empleado)
              .HasForeignKey(r => r.IdEmpleado);

            this.HasMany(e => e.Inspecciones)
            .WithRequired(i => i.Empleado)
            .HasForeignKey(i => i.IdEmpleado);
        }
    }
}
