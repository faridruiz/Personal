using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Practica.Models
{
    /// <summary>
    /// Modelo de facturación
    /// </summary>
    [Table("Facturas")]
    public class Factura : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Folio")]
        [Required(ErrorMessage = "Es necesario un folio")]
        public override int Identificador { get; set; }

        public int PedidoID { get; set; }
        /// <summary>
        /// Detalle de la deuda a facturar
        /// </summary>
        [ForeignKey("PedidoID")]
        public virtual Pedido Pedido { get; set; }
        /// <summary>
        /// Fecha de facturación
        /// </summary>
        [DataType(DataType.DateTime, ErrorMessage = "Ingrese una fecha válida")]
        [DisplayFormat(DataFormatString = "{0:g")]
        public DateTime Fecha { get; set; }

        
    }

}