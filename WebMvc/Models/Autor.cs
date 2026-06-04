using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace WebMvc.Models
{
    public class Autor
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        public List<Libro> ListaLibros{ get; set; }
    }
}
