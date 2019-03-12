namespace Practica.Migrations
{
    using Practica.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Practica.Models.PracticaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Practica.Models.PracticaContext";
        }

        protected override void Seed(Practica.Models.PracticaContext context)
        {
            var usuario = new Usuario() { Nombre_usuario = "admin", Contraseña = "1234" };

            #region Clientes
            var clientes = new List<Clientes>() {
                new Clientes {
                    Nombre_cliente =  "Jorge Mendez",
                    Direccion = "Boulevard Morelos Y Jose Vázques #1222",
                    Telefono = "6867896541",
                    TipoCliente = TipoCliente.Fisica
                },
                new Clientes {
                    Nombre_cliente =  "Empresa CONF S.A de C.V.",
                    Direccion = "Ave. Juarez #12",
                    Telefono = "6864396541",
                    TipoCliente = TipoCliente.Moral
                },
                new Clientes {
                    Nombre_cliente =  "Tramites GOB",
                    Direccion = "Calle Mordingo",
                    Telefono = "6865556541",
                    TipoCliente = TipoCliente.Moral
                },
                new Clientes {
                    Nombre_cliente =  "Los Temerarios ABC",
                    Direccion = "Ave. Los Escondidos #33B",
                    Telefono = "6861236541",
                    TipoCliente = TipoCliente.Moral
                },
                new Clientes {
                    Nombre_cliente =  "Antonio Rodríguez",
                    Direccion = "Callejón Benito Juarez interior #12B",
                    Telefono = "6869896541",
                    TipoCliente = TipoCliente.Fisica
                },
                new Clientes {
                    Nombre_cliente =  "Angel Rocío",
                    Direccion = "Adolfo López Mateo #12",
                    Telefono = "6867891521",
                    TipoCliente = TipoCliente.Fisica
                },
                new Clientes {
                    Nombre_cliente =  "Pedro Solas",
                    Direccion = "Ave. Niños Héroes #9",
                    Telefono = "6867896541",
                    TipoCliente = TipoCliente.Fisica
                }
            };
            #endregion

            #region Conceptos de pago
            var conceptos = new List<ConceptosPago>()
            {
                new ConceptosPago {
                    Concepto = "TCD",
                    Descripcion = "Tarjeta de crédito",
                },new ConceptosPago {
                    Concepto = "TDB",
                    Descripcion = "Tarjeta de débito",
                },new ConceptosPago {
                    Concepto = "VAL",
                    Descripcion = "Vales",
                },new ConceptosPago {
                    Concepto = "EFC",
                    Descripcion = "Efectivo",
                },new ConceptosPago {
                    Concepto = "CHQ",
                    Descripcion = "Cheques",
                },new ConceptosPago {
                    Concepto = "TRA",
                    Descripcion = "Transferencia bancaria",
                }
            };
            #endregion

            #region Pedidos
            var pedidos = new List<Pedido>()
            {
                new Pedido
                {
                    ClienteID = 1,
                    Concepto = "Consultas de inventario",
                    FechaPago = new DateTime(2019,03,11),
                    TipoBien = TipoBien.Servicio,
                    TotalAPagar = 50000
                },new Pedido
                {
                    ClienteID = 2,
                    Concepto = "Software de inventario",
                    FechaPago = new DateTime(2019,02,28),
                    TipoBien = TipoBien.Producto,
                    TotalAPagar = 150000
                },new Pedido
                {
                    ClienteID = 3,
                    Concepto = "Cajas",
                    FechaPago = new DateTime(2018,01,02),
                    TipoBien = TipoBien.Producto,
                    TotalAPagar = 5000
                },new Pedido
                {
                    ClienteID = 4,
                    Concepto = "Capacitación",
                    FechaPago = new DateTime(2019,04,20),
                    TipoBien = TipoBien.Servicio,
                    TotalAPagar = 38000
                },new Pedido
                {
                    ClienteID = 5,
                    Concepto = "Documentación de inventario",
                    FechaPago = new DateTime(2019,01,02),
                    TipoBien = TipoBien.Producto,
                    TotalAPagar = 15000
                },new Pedido
                {
                    ClienteID = 6,
                    Concepto = "Miscelaneo",
                    FechaPago = new DateTime(2019,01,02),
                    TipoBien = TipoBien.Producto,
                    TotalAPagar = 85000
                },new Pedido
                {
                    ClienteID = 7,
                    Concepto = "Capacitación",
                    FechaPago = new DateTime(2020,01,01),
                    TipoBien = TipoBien.Producto,
                    TotalAPagar = 50000
                },new Pedido
                {
                    ClienteID = 8,
                    Concepto = "Software",
                    FechaPago = new DateTime(2019,03,01),
                    TipoBien = TipoBien.Producto,
                    TotalAPagar = 50000
                },
            };
            #endregion


            context.Usuarios.AddOrUpdate(usuario);
            context.Clientes.AddOrUpdate(clientes.ToArray());
            context.ConceptosPago.AddOrUpdate(conceptos.ToArray());
            context.Pedidos.AddOrUpdate(pedidos.ToArray());
            context.SaveChanges();
        }
    }
}