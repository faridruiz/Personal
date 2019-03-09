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
    /// Usuarios para administración de la aplicación
    /// </summary>
    [Table("Usuarios")]
    public class Usuario : BaseModel
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int UsuarioID { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Usuario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nombre de usuario es requerido")]
        public string Nombre_usuario { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Contraseña es requerida")]
        public string Contraseña { get; set; }
    }
}