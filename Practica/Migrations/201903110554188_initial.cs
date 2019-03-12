namespace Practica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Identificador = c.Int(nullable: false, identity: true),
                        Nombre_cliente = c.String(nullable: false, maxLength: 50),
                        Telefono = c.String(nullable: false, maxLength: 20),
                        Direccion = c.String(nullable: false, maxLength: 100),
                        TipoCliente = c.Int(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Identificador);
            
            CreateTable(
                "dbo.ConceptosPago",
                c => new
                    {
                        Identificador = c.Int(nullable: false, identity: true),
                        Concepto = c.String(maxLength: 10),
                        Descripcion = c.String(),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Identificador);
            
            CreateTable(
                "dbo.Facturas",
                c => new
                    {
                        Identificador = c.Int(nullable: false),
                        PedidoID = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Identificador)
                .ForeignKey("dbo.Pedidos", t => t.PedidoID, cascadeDelete: true)
                .Index(t => t.PedidoID);
            
            CreateTable(
                "dbo.Pedidos",
                c => new
                    {
                        Identificador = c.Int(nullable: false, identity: true),
                        Concepto = c.String(nullable: false, maxLength: 50),
                        ClienteID = c.Int(nullable: false),
                        TipoBien = c.Int(nullable: false),
                        TotalAPagar = c.Double(nullable: false),
                        FechaPago = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Identificador)
                .ForeignKey("dbo.Clientes", t => t.ClienteID, cascadeDelete: true)
                .Index(t => t.ClienteID);
            
            CreateTable(
                "dbo.Pagos",
                c => new
                    {
                        Identificador = c.Int(nullable: false, identity: true),
                        PedidoID = c.Int(nullable: false),
                        TotalPago = c.Double(nullable: false),
                        Descripcion = c.String(),
                        TipoPago = c.Int(nullable: false),
                        ConceptoPagoID = c.Int(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Identificador)
                .ForeignKey("dbo.ConceptosPago", t => t.ConceptoPagoID, cascadeDelete: true)
                .ForeignKey("dbo.Pedidos", t => t.PedidoID, cascadeDelete: true)
                .Index(t => t.PedidoID)
                .Index(t => t.ConceptoPagoID);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Identificador = c.Int(nullable: false, identity: true),
                        Nombre_usuario = c.String(nullable: false),
                        Contraseña = c.String(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Identificador);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pagos", "PedidoID", "dbo.Pedidos");
            DropForeignKey("dbo.Pagos", "ConceptoPagoID", "dbo.ConceptosPago");
            DropForeignKey("dbo.Facturas", "PedidoID", "dbo.Pedidos");
            DropForeignKey("dbo.Pedidos", "ClienteID", "dbo.Clientes");
            DropIndex("dbo.Pagos", new[] { "ConceptoPagoID" });
            DropIndex("dbo.Pagos", new[] { "PedidoID" });
            DropIndex("dbo.Pedidos", new[] { "ClienteID" });
            DropIndex("dbo.Facturas", new[] { "PedidoID" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Pagos");
            DropTable("dbo.Pedidos");
            DropTable("dbo.Facturas");
            DropTable("dbo.ConceptosPago");
            DropTable("dbo.Clientes");
        }
    }
}
