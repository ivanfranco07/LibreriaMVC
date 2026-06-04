using System.ComponentModel.DataAnnotations;

namespace WebMvc.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        [Required]
        public string Descripcion { get; set; }
    }
}
