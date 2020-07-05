namespace RentCar.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregandoEstadoGoma : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inspecciones", "EstadoGomaDelanteraDerecha", c => c.Boolean(nullable: false));
            AddColumn("dbo.Inspecciones", "EstadoGomaDelanteraIzquierda", c => c.Boolean(nullable: false));
            AddColumn("dbo.Inspecciones", "EstadoGomaTraseraDerecha", c => c.Boolean(nullable: false));
            AddColumn("dbo.Inspecciones", "EstadoGomaTraseraIzquierda", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inspecciones", "EstadoGomaTraseraIzquierda");
            DropColumn("dbo.Inspecciones", "EstadoGomaTraseraDerecha");
            DropColumn("dbo.Inspecciones", "EstadoGomaDelanteraIzquierda");
            DropColumn("dbo.Inspecciones", "EstadoGomaDelanteraDerecha");
        }
    }
}
