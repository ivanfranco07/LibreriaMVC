using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMvc.Models
{
    public class Libro
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        
        [Required]
        [Display(Name ="ISBN")]
        public string Isbn { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Editorial { get; set; }
        
        [Required]
        [Display(Name ="Cantidad de páginas")]
        public int CantidadDePaginas { get; set; }
        
        [Required]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Precio { get; set; }
        
        [Url]
        [Required]
        [Display(Name = "Tapa del libro")]
        public string UrlImagenTapa { get; set; }
        
        public string Sinopsis { get; set; }
        
        [StringLength(100)]
        public string Idioma { get; set; }
        
        [DisplayFormat(DataFormatString= "{0:yyyy/MM/dd}", ApplyFormatInEditMode= true)]
        [Display(Name ="Publicación")]
        public DateTime FechaPublicacion { get; set; }

        [Display(Name ="Lista de autores")]
        public List<Autor>? ListaAutores { get; set; }
        //[NotMapped]
        
        public int CategoriaId { get; set; }
        
        public Categoria? Categoria { get; set; }
    }
}
