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
    public class RegistroViewModel
    {
        [Required(ErrorMessage ="Debes ingresar un nombre.")]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required(ErrorMessage ="Debes ingresar un apellido.")]
        [StringLength(50)]
        public string Apellido { get; set; }
        [EmailAddress(ErrorMessage ="Debes ingresar un email válido.")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="La constraseña es obligatoria.")]
        public string Clave { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Debes confirmar la clave")]
        [Compare("Clave", ErrorMessage ="Las claves no coinciden.")]
        public string ConfimarClave { get; set; }
    }
    public class ProfileViewModel
    {
        [Required(ErrorMessage = "Debes ingresar un nombre.")]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debes ingresar un apellido.")]
        [StringLength(50)]
        public string Apellido { get; set; }
        [EmailAddress(ErrorMessage = "Debes ingresar un email válido.")]
        public string Email { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }
    }
}
