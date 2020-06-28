namespace RentCar.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Cedula = c.String(),
                        NoTarjetaCr = c.String(),
                        LimiteCredito = c.Int(nullable: false),
                        IdTipoPersona = c.Int(nullable: false),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TiposPersonas", t => t.IdTipoPersona, cascadeDelete: true)
                .Index(t => t.IdTipoPersona);
            
            CreateTable(
                "dbo.Inspecciones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdVehiculo = c.Int(nullable: false),
                        IdCliente = c.Int(nullable: false),
                        IdEmpleado = c.Int(nullable: false),
                        TieneRalladura = c.Boolean(nullable: false),
                        CantidadCombustible = c.String(),
                        TieneGomaRespuesta = c.Boolean(nullable: false),
                        TieneGato = c.Boolean(nullable: false),
                        TieneRoturasCristal = c.Boolean(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empleados", t => t.IdEmpleado, cascadeDelete: true)
                .ForeignKey("dbo.Vehiculos", t => t.IdVehiculo, cascadeDelete: true)
                .ForeignKey("dbo.Clientes", t => t.IdCliente, cascadeDelete: true)
                .Index(t => t.IdVehiculo)
                .Index(t => t.IdCliente)
                .Index(t => t.IdEmpleado);
            
            CreateTable(
                "dbo.Empleados",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Cedula = c.String(),
                        TandaLabor = c.String(),
                        PorcientoComision = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FechaIngreso = c.DateTime(nullable: false),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rentas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdEmpleado = c.Int(nullable: false),
                        IdVehiculo = c.Int(nullable: false),
                        IdCliente = c.Int(nullable: false),
                        FechaRenta = c.DateTime(nullable: false),
                        FechaDevolucion = c.DateTime(),
                        MontoDiario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CantidadDias = c.Int(nullable: false),
                        Comentario = c.String(),
                        Estado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vehiculos", t => t.IdVehiculo, cascadeDelete: true)
                .ForeignKey("dbo.Empleados", t => t.IdEmpleado, cascadeDelete: true)
                .ForeignKey("dbo.Clientes", t => t.IdCliente, cascadeDelete: true)
                .Index(t => t.IdEmpleado)
                .Index(t => t.IdVehiculo)
                .Index(t => t.IdCliente);
            
            CreateTable(
                "dbo.Vehiculos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        Chasis = c.String(),
                        NoMotor = c.String(),
                        Placa = c.String(),
                        IdTipoVehiculo = c.Int(nullable: false),
                        IdMarca = c.Int(nullable: false),
                        IdModelo = c.Int(nullable: false),
                        IdTipoCombustible = c.Int(nullable: false),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Modelos", t => t.IdModelo)
                .ForeignKey("dbo.Marcas", t => t.IdMarca, cascadeDelete: true)
                .ForeignKey("dbo.TiposCombustibles", t => t.IdTipoCombustible, cascadeDelete: true)
                .ForeignKey("dbo.TiposVehiculos", t => t.IdTipoVehiculo, cascadeDelete: true)
                .Index(t => t.IdTipoVehiculo)
                .Index(t => t.IdMarca)
                .Index(t => t.IdModelo)
                .Index(t => t.IdTipoCombustible);
            
            CreateTable(
                "dbo.Marcas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Modelos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdMarca = c.Int(nullable: false),
                        Descripcion = c.String(),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Marcas", t => t.IdMarca, cascadeDelete: true)
                .Index(t => t.IdMarca);
            
            CreateTable(
                "dbo.TiposCombustibles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TiposVehiculos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TiposPersonas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tipo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clientes", "IdTipoPersona", "dbo.TiposPersonas");
            DropForeignKey("dbo.Rentas", "IdCliente", "dbo.Clientes");
            DropForeignKey("dbo.Inspecciones", "IdCliente", "dbo.Clientes");
            DropForeignKey("dbo.Rentas", "IdEmpleado", "dbo.Empleados");
            DropForeignKey("dbo.Vehiculos", "IdTipoVehiculo", "dbo.TiposVehiculos");
            DropForeignKey("dbo.Vehiculos", "IdTipoCombustible", "dbo.TiposCombustibles");
            DropForeignKey("dbo.Rentas", "IdVehiculo", "dbo.Vehiculos");
            DropForeignKey("dbo.Vehiculos", "IdMarca", "dbo.Marcas");
            DropForeignKey("dbo.Modelos", "IdMarca", "dbo.Marcas");
            DropForeignKey("dbo.Vehiculos", "IdModelo", "dbo.Modelos");
            DropForeignKey("dbo.Inspecciones", "IdVehiculo", "dbo.Vehiculos");
            DropForeignKey("dbo.Inspecciones", "IdEmpleado", "dbo.Empleados");
            DropIndex("dbo.Modelos", new[] { "IdMarca" });
            DropIndex("dbo.Vehiculos", new[] { "IdTipoCombustible" });
            DropIndex("dbo.Vehiculos", new[] { "IdModelo" });
            DropIndex("dbo.Vehiculos", new[] { "IdMarca" });
            DropIndex("dbo.Vehiculos", new[] { "IdTipoVehiculo" });
            DropIndex("dbo.Rentas", new[] { "IdCliente" });
            DropIndex("dbo.Rentas", new[] { "IdVehiculo" });
            DropIndex("dbo.Rentas", new[] { "IdEmpleado" });
            DropIndex("dbo.Inspecciones", new[] { "IdEmpleado" });
            DropIndex("dbo.Inspecciones", new[] { "IdCliente" });
            DropIndex("dbo.Inspecciones", new[] { "IdVehiculo" });
            DropIndex("dbo.Clientes", new[] { "IdTipoPersona" });
            DropTable("dbo.TiposPersonas");
            DropTable("dbo.TiposVehiculos");
            DropTable("dbo.TiposCombustibles");
            DropTable("dbo.Modelos");
            DropTable("dbo.Marcas");
            DropTable("dbo.Vehiculos");
            DropTable("dbo.Rentas");
            DropTable("dbo.Empleados");
            DropTable("dbo.Inspecciones");
            DropTable("dbo.Clientes");
        }
    }
}
