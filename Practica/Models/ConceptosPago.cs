using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Practica.Models
{
    /// <summary>
    /// 
    /// </summary>
    [Table("ConceptosPago")]
    public class ConceptosPago : BaseModel
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int ConceptoID { get; set; }

        /// <summary>
        /// Modo de pago con el que se registrará el pago y/o abono.
        /// </summary>
        [DataType(DataType.Text)]
        [MinLength(3,ErrorMessage = "Ingrese un concepto de al menos tres caracteres.")]
        [MaxLength(10, ErrorMessage = "El concepto supera el límite de caracteres permitidos.")]
        public string Concepto { get; set; }
        /// <summary>
        /// Descripción a detalle del concepto de pago
        /// </summary>
        [DataType(DataType.MultilineText)]
        [MinLength(3, ErrorMessage = "Escriba al menos una palabra para la descripción.")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        /// <summary>
        /// Diferencía entre un pago y un abono para el 
        /// </summary>
        public TipoConceptoPago Tipo { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum TipoConceptoPago
    {
        Abono,
        Cargo
    }
}