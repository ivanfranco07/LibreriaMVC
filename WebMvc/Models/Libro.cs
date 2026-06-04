namespace WebMvc.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public string Isbn { get; set; }
        public string Editorial { get; set; }
        public int CantidadDePaginas { get; set; }
        public double Precio { get; set; }
        public string Sinopsis { get; set; }
        public string Idioma { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public int IdAutor { get; set; }
        public Autor? Autor { get; set; }
        public int IdCategoria { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
