using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Practica.Models
{
    /// <summary>
    /// Detalle de la deuda con relación a cliente
    /// </summary>   
    [Table("Pedidos")]
    public class Pedido : BaseModel
    {

        /// <summary>
        /// Es el producto o servicio
        /// </summary>
        [DataType(DataType.Text)]
        [MinLength(3, ErrorMessage = "Ingrese al menos una palabra para el concepto.")]
        [MaxLength(50, ErrorMessage = "Los caracteres ingresados superan el límite permitido")]
        [Display(Name = "Nombre del producto/servicio")]
        [Required(ErrorMessage = "Es necesario que ingrese un concepto de producto o servicio")]
        public string Concepto { get; set; }

        [Required(ErrorMessage = "Es necesario un cliente para asociar el pedido")]
        [Display(Name = "Cliente")]
        public int ClienteID { get; set; }
        /// <summary>
        /// Relación con la tabla de clientes
        /// </summary>
        [ForeignKey("ClienteID")]
        [Display(Name = "Cliente")]
        public virtual Clientes Cliente { get; set; }

        /// <summary>
        /// Tipo de bien; producto o servicio
        /// </summary>
        [Display(Name = "Tipo")]
        public TipoBien TipoBien { get; set; }
                               
        [Display(Name = "Total a pagar")]
        [DataType(DataType.Currency, ErrorMessage = "Ingrese un valor válido para el pago")]
        [Required(ErrorMessage = "Es necesario que se establezca el valor a pagar por el bien/servicio")]
        public double TotalAPagar { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Ingrese una fecha válida")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Fecha de pago")]
        [Required(ErrorMessage = "Es necesario ingresar una fecha de pago")]
        public DateTime FechaPago { get; set; }

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
}