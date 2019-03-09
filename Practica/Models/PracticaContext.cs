namespace Practica.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PracticaContext : DbContext
    {
        public PracticaContext() : base("name=PracticaContext")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }
        public DbSet<Pedido> Pedidos { get; set; }        

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Clientes> Clientes { get; set; }

        public DbSet<ConceptosPago> ConceptosPago { get; set; }

        public DbSet<Factura> Facturas { get; set; }

        public DbSet<Pagos> Pagos { get; set; }
    }
}
