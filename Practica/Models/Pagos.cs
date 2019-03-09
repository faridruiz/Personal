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
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int PagoID { get; set; }
                
        public int PedidoID { get; set; }

        [ForeignKey("PedidoID")]        
        public virtual Pedido Pedido { get; set; }        
        
        [Display(Name = "Total pagado")]
        [DataType(DataType.Currency, ErrorMessage = "Ingrese valor válido para el campo de pago")]
        [MinLength(1, ErrorMessage = "Ingrese al menos un digito para registrar el pago")]
        public double TotalPagado { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.Text)]
        [MinLength(2, ErrorMessage = "Ingrese al menos una palabra")]
        public string Descripcion { get; set; }        

    }
}