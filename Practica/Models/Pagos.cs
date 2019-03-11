using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Practica.Models
{
    [Table("Pagos")]
    public class Pagos : BaseModel
    {
        [Display(Name ="Pedido")]
        public int PedidoID { get; set; }

        [ForeignKey("PedidoID")]
        [Display(Name = "Pedido")]
        public virtual Pedido Pedido { get; set; }        
        
        [Display(Name = "Total de pago")]
        [DataType(DataType.Currency, ErrorMessage = "Ingrese valor válido para el campo de pago")]
        public double TotalPago { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.Text)]
        [MinLength(2, ErrorMessage = "Ingrese al menos una palabra")]
        public string Descripcion { get; set; }
                
        [Display(Name = "Tipo de pago")]
        [Required]
        public TipoPago TipoPago { get; set; }

        [Display(Name = "Concepto de pago")]
        [Required(ErrorMessage = "Es necesario seleccionar un método de pago")]
        public int ConceptoPagoID { get; set; }

        [ForeignKey("ConceptoPagoID")]
        [Display(Name = "Concepto de pago")]
        public virtual ConceptosPago ConceptoDePago { get; set; }

    }

    public enum TipoPago
    {
        Abono,
        Pago
    }
}