namespace Practica.Migrations
{
    using Practica.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Practica.Models.PracticaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Practica.Models.PracticaContext";
        }

        protected override void Seed(Practica.Models.PracticaContext context)
        {
            var usuario = new Usuario() { UsuarioID = 1, Nombre_usuario = "admin", Contraseña = "1234" };
            context.Usuarios.AddOrUpdate(usuario);
            context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
