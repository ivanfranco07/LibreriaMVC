using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMvc.Models
{
    public class Libro
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Isbn { get; set; }
        [Required]
        public string Editorial { get; set; }
        [Required]
        public int CantidadDePaginas { get; set; }
        [Required]
        public double Precio { get; set; }
        [Url]
        [Required]
        public string UrlImagenTapa { get; set; }
        public string Sinopsis { get; set; }
        public string Idioma { get; set; }
        [DisplayFormat(DataFormatString= "{0:dd/MM/yyyy}", ApplyFormatInEditMode= true)]
        public DateTime FechaPublicacion { get; set; }
        public List<Autor>? ListaAutores { get; set; }
        //[NotMapped]
        public int IdCategoria { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
