using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practica.Models
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Usuario")]
    public class Usuario
    {
        [Key]        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Nombre de usuario es requerido")]
        [DataType(DataType.Text)]
        [Display(Name = "Usuario")]
        public string Nombre_usuario { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Contraseña es requerida")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Contraseña { get; set; }
    }
    /// <summary>
    /// Modelo con la información del cliente
    /// </summary>
    [Table("Cliente")]
    public class Clientes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClienteID { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(50, ErrorMessage = "El nombre de cliente sobrepasa el limite de caracteres.")]
        public string Nombre_cliente { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(20, ErrorMessage = "El teléfono sobrepasa el limite de caracteres.")]
        public string Telefono { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(100, ErrorMessage = "La dirección sobrepasa el limite de caracteres.")]
        public string Direccion { get; set; }
        
        public TipoCliente TipoCliente { get; set; }

        /// <summary>
        /// Facturas realizadas por el cliente. 
        /// </summary>
        public virtual ICollection<Factura> Facturas { get; set; }
    }
    /// <summary>
    /// Detalle de la deuda con relación a cliente
    /// </summary>   
    public class DetalleDeuda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeudaID { get; set; }

        /// <summary>
        /// Es el producto o servicio
        /// </summary>
        public string Concepto { get; set; } 

        [ForeignKey("Cliente")]
        public int ClienteID { get; set; }

        public virtual Clientes Cliente { get; set; }
        /// <summary>
        /// Tipo de bien; producto o servicio
        /// </summary>
        public TipoBien TipoBien { get; set; }
    }
    /// <summary>
    /// Modelo de facturación
    /// </summary>
    public class Factura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Folio { get; set; }
        /// <summary>
        /// Detalle de la deuda a facturar
        /// </summary>
        public virtual DetalleDeuda Detalle { get; set; }
        /// <summary>
        /// Fecha de facturación
        /// </summary>
        public DateTime Fecha { get; set; }        
    }

    public class ConceptosPago
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConceptoID { get; set; }

        public string Concepto { get; set; }
        public string Descripcion { get; set; }

        public int Tipo { get; set; }
    }
    /// <summary>
    /// Producto = 0,
    /// Servicio = 1 
    /// </summary>
    public enum TipoBien
    {
        Producto,
        Servicio
    }
    /// <summary>
    /// Tipo de persona que identific al cliente;
    /// Moral = 0,
    /// Fisica = 1
    /// </summary>
    public enum TipoCliente
    {
        Moral,
        Fisica
    }
    /// <summary>
    /// 
    /// </summary>
    public enum TipoConceptoPago
    {
        Abono,
        Cargo
    }
    
    /// <summary>
    /// Contexto para base de datos
    /// </summary>
    public class PracticaContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
    }
    

}