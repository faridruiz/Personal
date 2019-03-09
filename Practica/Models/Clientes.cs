using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Practica.Models
{
    /// <summary>
    /// Modelo con la información del cliente
    /// </summary>
    [Table("Clientes")]
    public class Clientes : BaseModel
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int ClienteID { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(50, ErrorMessage = "El nombre de cliente sobrepasa el limite de caracteres.")]
        [MinLength(3, ErrorMessage = "Ingrese al menos una palabra para el nombre.")]
        [Display(Name = "Nombre cliente")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Es necesario un nombre")]
        public string Nombre_cliente { get; set; }

        [DataType(DataType.PhoneNumber)]
        [MaxLength(20, ErrorMessage = "El teléfono sobrepasa el limite de caracteres.")]
        [MinLength(10, ErrorMessage = "Ingrese un número de télefono válido.")]
        [Display(Name = "Teléfono")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo de teléfono es obligatorio")]
        public string Telefono { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(100, ErrorMessage = "La dirección sobrepasa el limite de caracteres.")]
        [MinLength(3, ErrorMessage = "Ingrese al menos una palabra para la dirección")]
        [Display(Name = "Dirección")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo de dirección es obligatorio")]
        public string Direccion { get; set; }

        /// <summary>
        /// Establece que tipo de cliente pertenece el registro 
        /// </summary>
        [Display(Name = "Tipo de cliente")]
        public TipoCliente TipoCliente { get; set; }

        /// <summary>
        /// Facturas solicitadas por el cliente. 
        /// </summary>
        public virtual ICollection<Factura> Facturas { get; }
        /// <summary>
        /// Pagos realizados y/o abono realizado por el cliente
        /// </summary>
        public virtual ICollection<Pagos> Pagos { get; }
        
    }

    /// <summary>
    /// Tipo de persona que identific al cliente;
    /// Moral = 0,
    /// Fisica = 1
    /// </summary>
    public enum TipoCliente
    {
        Moral,
        [Display(Name = "Física")]
        Fisica
    }
}