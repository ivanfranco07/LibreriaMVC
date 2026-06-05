using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMvc.Models
{
    public class Favorito
    {
        public int Id { get; set; }
        public int LibroId { get; set; }
        public Libro? Libro { get; set; }
        //AspNetUsers tiene una columna Id que es de tipo GUID en formato texto, por eso UsuarioId es string
        public string UsuarioId { get; set; }
        /*
        Forma de avisarle a EF que UsuarioId es la FK que relaciona Review con Usuario, 
        y así evitar que cree una 2da columna 
        */
        [ForeignKey("UsuarioId")]
        public Usuario? Usuario { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
    }
}
