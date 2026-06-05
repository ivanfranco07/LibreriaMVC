using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebMvc.Models;

namespace WebMvc.Data
{
    //Seeder con GitHub Copilot
    public static class DbSeeder
    {
        public static async Task SeedAsync(
            LibreriaDbContext context,
            UserManager<Usuario> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            // Aplicar migraciones pendientes (si corresponde)
            try
            {
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en migración: {ex.Message}");
                throw; // relanzá la excepción para que no continúe
            }

            // 1) Categorías
            if (!context.Categorias.Any())
            {
                var categorias = new[]
                {
                    "Ficción", "No ficción", "Ciencia ficción", "Fantasía", "Misterio",
                    "Thriller", "Romance", "Histórica", "Biografía", "Infantil",
                    "Juvenil", "Educación", "Tecnología", "Arte", "Ciencia",
                    "Salud", "Autoayuda", "Negocios", "Viajes", "Religión",
                    "Política", "Poesía", "Clásicos", "Humor", "Cómics"
                }.Select(d => new Categoria { Descripcion = d }).ToArray();

                context.Categorias.AddRange(categorias);
                await context.SaveChangesAsync();
            }

            // 2) Autores
            if (!context.Autores.Any())
            {
                var autores = new[]
                {
                    new Autor { Nombre = "Gabriel", Apellido = "García Márquez", ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Isabel", Apellido = "Allende", ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "J. R. R.", Apellido = "Tolkien", ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Stephen", Apellido = "King", ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Yuval Noah", Apellido = "Harari", ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Agatha", Apellido = "Christie", ListaLibros = new List<Libro>() },
                };

                context.Autores.AddRange(autores);
                await context.SaveChangesAsync();
            }

            // Recuperar listas ya persistidas para asegurar relaciones correctas
            var categoriasPersistidas = await context.Categorias.ToListAsync();
            var autoresPersistidos = await context.Autores.ToListAsync();

            // Helper para buscar categoría por descripción
            Categoria Cat(string descripcion) =>
                categoriasPersistidas.FirstOrDefault(c => c.Descripcion.Equals(descripcion, StringComparison.OrdinalIgnoreCase))
                ?? categoriasPersistidas.First(); // fallback

            // Helper para buscar autor por apellido (o nombre compuesto)
            Autor Aut(string apellidoStartsWith) =>
                autoresPersistidos.FirstOrDefault(a => a.Apellido.StartsWith(apellidoStartsWith, StringComparison.OrdinalIgnoreCase))
                ?? autoresPersistidos.First();

            // 3) Libros (varios libros con composición de autores y categorías)
            if (!context.Libros.Any())
            {
                var libros = new List<Libro>
                {
                    new Libro
                    {
                        Nombre = "Cien años de soledad",
                        Isbn = "978-0307474728",
                        Editorial = "Sudamericana",
                        CantidadDePaginas = 417,
                        Precio = 19.90,
                        UrlImagenTapa = "https://example.com/cien_anos.jpg",
                        Sinopsis = "Saga familiar y realismo mágico en Macondo.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1967, 5, 30),
                        ListaAutores = new List<Autor>{ Aut("García Márquez") },
                        Categoria = Cat("Ficción")
                    },
                    new Libro
                    {
                        Nombre = "La casa de los espíritus",
                        Isbn = "978-1447242347",
                        Editorial = "Plaza & Janés",
                        CantidadDePaginas = 448,
                        Precio = 17.50,
                        UrlImagenTapa = "https://example.com/casa_espiritus.jpg",
                        Sinopsis = "Crónica familiar que combina lo político y lo mágico.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1982, 1, 1),
                        ListaAutores = new List<Autor>{ Aut("Allende") },
                        Categoria = Cat("Ficción")
                    },
                    new Libro
                    {
                        Nombre = "El hobbit",
                        Isbn = "978-0261103344",
                        Editorial = "Allen & Unwin",
                        CantidadDePaginas = 310,
                        Precio = 14.00,
                        UrlImagenTapa = "https://example.com/el_hobbit.jpg",
                        Sinopsis = "Aventura de Bilbo Bolsón en la Tierra Media.",
                        Idioma = "Inglés",
                        FechaPublicacion = new DateTime(1937, 9, 21),
                        ListaAutores = new List<Autor>{ Aut("Tolkien") },
                        Categoria = Cat("Fantasía")
                    },
                    new Libro
                    {
                        Nombre = "It",
                        Isbn = "978-1501142970",
                        Editorial = "Viking",
                        CantidadDePaginas = 1138,
                        Precio = 22.00,
                        UrlImagenTapa = "https://example.com/it.jpg",
                        Sinopsis = "Novela de terror sobre un ente que acecha a un pueblo.",
                        Idioma = "Inglés",
                        FechaPublicacion = new DateTime(1986, 9, 15),
                        ListaAutores = new List<Autor>{ Aut("King") },
                        Categoria = Cat("Thriller")
                    },
                    new Libro
                    {
                        Nombre = "Sapiens: De animales a dioses",
                        Isbn = "978-0062316097",
                        Editorial = "Harper",
                        CantidadDePaginas = 498,
                        Precio = 18.99,
                        UrlImagenTapa = "https://example.com/sapiens.jpg",
                        Sinopsis = "Breve historia de la humanidad.",
                        Idioma = "Inglés",
                        FechaPublicacion = new DateTime(2011, 6, 4),
                        ListaAutores = new List<Autor>{ Aut("Harari") },
                        Categoria = Cat("Ciencia")
                    },
                    new Libro
                    {
                        Nombre = "Asesinato en el Orient Express",
                        Isbn = "978-0007119318",
                        Editorial = "Collins Crime Club",
                        CantidadDePaginas = 256,
                        Precio = 9.50,
                        UrlImagenTapa = "https://example.com/orient_express.jpg",
                        Sinopsis = "Un clásico misterio de Agatha Christie.",
                        Idioma = "Inglés",
                        FechaPublicacion = new DateTime(1934, 1, 1),
                        ListaAutores = new List<Autor>{ Aut("Christie") },
                        Categoria = Cat("Misterio")
                    },
                    // Libro con múltiples autores (ejemplo de composición)
                    new Libro
                    {
                        Nombre = "Antología: grandes relatos",
                        Isbn = "978-1234567890",
                        Editorial = "Editorial Ejemplo",
                        CantidadDePaginas = 320,
                        Precio = 12.00,
                        UrlImagenTapa = "https://example.com/antologia.jpg",
                        Sinopsis = "Colección de relatos de varios autores.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(2020, 1, 1),
                        ListaAutores = new List<Autor>{ Aut("García Márquez"), Aut("Allende") },
                        Categoria = Cat("Clásicos")
                    }
                };

                context.Libros.AddRange(libros);
                await context.SaveChangesAsync();
            }

            // 4) Rol admin y usuario administrador
            const string adminRoleName = "Admin";
            const string adminEmail = "admin@libreria.local";
            const string adminPassword = "Admin123!"; // Cambiar en producción

            if (!await roleManager.RoleExistsAsync(adminRoleName))
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole(adminRoleName));
                // podés comprobar roleResult.Succeeded si necesitás logs
            }

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new Usuario
                {
                    UserName = "admin",
                    Email = adminEmail,
                    EmailConfirmed = true,
                    Nombre = "Administrador",
                    Apellido = "Sistema",
                    FechaNacimiento = DateTime.UtcNow.AddYears(-30)
                };

                var userResult = await userManager.CreateAsync(adminUser, adminPassword);
                if (userResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, adminRoleName);
                }
                else
                {
                    // Opcional: lanzar excepción o registrar errores en logs
                    // throw new Exception($"No se pudo crear el usuario admin: {string.Join(", ", userResult.Errors.Select(e => e.Description))}");
                }
            }
            else
            {
                // Asegurar que el usuario esté en el rol admin
                if (!await userManager.IsInRoleAsync(adminUser, adminRoleName))
                {
                    await userManager.AddToRoleAsync(adminUser, adminRoleName);
                }
            }
        }
    }
}
