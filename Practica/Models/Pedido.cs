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
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int PedidoID { get; set; }

        /// <summary>
        /// Es el producto o servicio
        /// </summary>
        [DataType(DataType.Text)]
        [MinLength(3, ErrorMessage = "Ingrese al menos una palabra para el concepto.")]
        [MaxLength(25, ErrorMessage = "Los caracteres ingresados superan el límite permitido")]
        public string Concepto { get; set; }

        public int ClienteID { get; set; }
        /// <summary>
        /// Relación con la tabla de clientes
        /// </summary>
        [ForeignKey("ClienteID")]
        public virtual Clientes Cliente { get; set; }
        /// <summary>
        /// Tipo de bien; producto o servicio
        /// </summary>
        public TipoBien TipoBien { get; set; }

        public int ConceptoPagoID { get; set; }

        [ForeignKey("ConceptoPagoID")]
        public virtual ConceptosPago TipoDePago { get; set; }
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