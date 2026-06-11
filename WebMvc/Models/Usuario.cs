using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebMvc.Models
{
    /*
    Extensión de la clase Usuario de Identity, lo que permite personalizar y agregar
    columnas propias a la tabla AspNetUsers.    
    */
    public class Usuario : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }
        public List<Favorito>? PeliculasFavoritas { get; set; }
        public List<Review>? ReviewsUsuario { get; set; }
    }
    public class LogInViewModel
    {
        [EmailAddress(ErrorMessage ="Ingresar un email válido")]
        [Required(ErrorMessage ="El correo es obligatori")]
        public string Email{ get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="La clave es obligatoria")]
        public string Clave { get; set; }
        public bool Recordarme { get; set; }
    }
}
