using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebMvc.Models;

namespace WebMvc.Data
{
    public class LibreriaDbContext : IdentityDbContext
    {
        public LibreriaDbContext(DbContextOptions options) : base(options){}

        public DbSet<Libro> Libros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

    }
}
