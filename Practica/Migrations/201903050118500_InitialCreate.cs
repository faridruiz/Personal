namespace Practica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        ClienteID = c.Int(nullable: false, identity: true),
                        Nombre_cliente = c.String(maxLength: 50),
                        Telefono = c.String(maxLength: 20),
                        Direccion = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ClienteID);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        UsuarioID = c.Int(nullable: false, identity: true),
                        Nombre_usuario = c.String(nullable: false),
                        Contraseña = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UsuarioID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Usuario");
            DropTable("dbo.Cliente");
        }
    }
}
