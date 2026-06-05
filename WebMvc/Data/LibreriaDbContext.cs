using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebMvc.Models;

namespace WebMvc.Data
{
    /*
    Con IdentityDbContext<Usuario> le decís a EF que la tabla AspNetUsers debe mapear a 
    tu clase Usuario (que hereda de IdentityUser) con todas sus propiedades extra. 
    Así las columnas adicionales que agregaste se reflejan en la base de datos 
    */
    public class LibreriaDbContext : IdentityDbContext<Usuario>
    {
        public LibreriaDbContext(DbContextOptions options) : base(options){}

        public DbSet<Libro> Libros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Favorito> Favoritos { get; set; }
        public DbSet<Review> Reviews { get; set; }

    }
}
