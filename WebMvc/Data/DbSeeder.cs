using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebMvc.Models;

namespace WebMvc.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(
            LibreriaDbContext context,
            UserManager<Usuario> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            try
            {
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en migración: {ex.Message}");
                throw;
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
                    new Autor { Nombre = "Gabriel",      Apellido = "García Márquez",  ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "J. R. R.",     Apellido = "Tolkien",          ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "J. K.",        Apellido = "Rowling",          ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "George",       Apellido = "Orwell",           ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Antoine de",   Apellido = "Saint-Exupéry",    ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Fiódor",       Apellido = "Dostoievski",      ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Jane",         Apellido = "Austen",           ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Dan",          Apellido = "Brown",            ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Ray",          Apellido = "Bradbury",         ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Paulo",        Apellido = "Coelho",           ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Frank",        Apellido = "Herbert",          ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Yuval Noah",   Apellido = "Harari",           ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Umberto",      Apellido = "Eco",              ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "León",         Apellido = "Tolstói",          ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "William",      Apellido = "Gibson",           ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Harper",       Apellido = "Lee",              ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "F. Scott",     Apellido = "Fitzgerald",       ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Aldous",       Apellido = "Huxley",           ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Victor",       Apellido = "Hugo",             ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Franz",        Apellido = "Kafka",            ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Isaac",        Apellido = "Asimov",           ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Oscar",        Apellido = "Wilde",            ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Julio",        Apellido = "Cortázar",         ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Ernesto",      Apellido = "Sabato",           ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Homero",       Apellido = "",                 ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Eckhart",      Apellido = "Tolle",            ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Orson Scott",  Apellido = "Card",             ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Hermann",      Apellido = "Hesse",            ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Jorge Luis",   Apellido = "Borges",           ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Carlos Ruiz",  Apellido = "Zafón",            ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Carl",         Apellido = "Sagan",            ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Stephen",      Apellido = "Hawking",          ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Juan",         Apellido = "Rulfo",            ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "San",          Apellido = "Agustín",          ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Federico G.",  Apellido = "Lorca",            ListaLibros = new List<Libro>() },
                    new Autor { Nombre = "Miguel de",    Apellido = "Cervantes",        ListaLibros = new List<Libro>() },
                };

                context.Autores.AddRange(autores);
                await context.SaveChangesAsync();
            }

            var categoriasPersistidas = await context.Categorias.ToListAsync();
            var autoresPersistidos = await context.Autores.ToListAsync();

            Categoria Cat(string descripcion) =>
                categoriasPersistidas.FirstOrDefault(c => c.Descripcion.Equals(descripcion, StringComparison.OrdinalIgnoreCase))
                ?? categoriasPersistidas.First();

            Autor Aut(string apellido) =>
                autoresPersistidos.FirstOrDefault(a => a.Apellido.StartsWith(apellido, StringComparison.OrdinalIgnoreCase))
                ?? autoresPersistidos.First();

            // 3) Libros
            if (!context.Libros.Any())
            {
                var libros = new List<Libro>
                {
                    // ── Batch 1 ──────────────────────────────────────────────────────────
                    new Libro
                    {
                        Nombre = "El señor de los anillos",
                        Isbn = "978-8445071656",
                        Editorial = "Minotauro",
                        CantidadDePaginas = 1200,
                        Precio = 29.99,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788445071656-L.jpg",
                        Sinopsis = "La épica historia de la Tierra Media y la lucha contra Sauron.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1954, 7, 29),
                        ListaAutores = new List<Autor> { Aut("Tolkien") },
                        Categoria = Cat("Fantasía")
                    },
                    new Libro
                    {
                        Nombre = "Harry Potter y la piedra filosofal",
                        Isbn = "978-8478884452",
                        Editorial = "Salamandra",
                        CantidadDePaginas = 309,
                        Precio = 19.99,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788478884452-L.jpg",
                        Sinopsis = "Un joven descubre que es mago y asiste a la escuela Hogwarts.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1997, 6, 26),
                        ListaAutores = new List<Autor> { Aut("Rowling") },
                        Categoria = Cat("Fantasía")
                    },
                    new Libro
                    {
                        Nombre = "Cien años de soledad",
                        Isbn = "978-8497592208",
                        Editorial = "Cátedra",
                        CantidadDePaginas = 471,
                        Precio = 22.50,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788497592208-L.jpg",
                        Sinopsis = "La saga de la familia Buendía en el pueblo ficticio de Macondo.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1967, 5, 30),
                        ListaAutores = new List<Autor> { Aut("García Márquez") },
                        Categoria = Cat("Ficción")
                    },
                    new Libro
                    {
                        Nombre = "Don Quijote de la Mancha",
                        Isbn = "978-8424922580",
                        Editorial = "Espasa",
                        CantidadDePaginas = 1340,
                        Precio = 35.00,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788424922580-L.jpg",
                        Sinopsis = "Las aventuras del caballero andante Alonso Quijano.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1605, 1, 16),
                        ListaAutores = new List<Autor> { Aut("Cervantes") },
                        Categoria = Cat("Clásicos")
                    },
                    new Libro
                    {
                        Nombre = "1984",
                        Isbn = "978-8499890272",
                        Editorial = "Destino",
                        CantidadDePaginas = 368,
                        Precio = 18.75,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788499890272-L.jpg",
                        Sinopsis = "Una distopía sobre el totalitarismo y la vigilancia del Estado.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1949, 6, 8),
                        ListaAutores = new List<Autor> { Aut("Orwell") },
                        Categoria = Cat("Ciencia ficción")
                    },
                    new Libro
                    {
                        Nombre = "El principito",
                        Isbn = "978-8498381498",
                        Editorial = "Salamandra",
                        CantidadDePaginas = 96,
                        Precio = 12.99,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788498381498-L.jpg",
                        Sinopsis = "Un piloto conoce a un pequeño príncipe llegado de otro planeta.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1943, 4, 6),
                        ListaAutores = new List<Autor> { Aut("Saint-Exupéry") },
                        Categoria = Cat("Ficción")
                    },
                    new Libro
                    {
                        Nombre = "Crimen y castigo",
                        Isbn = "978-8420674209",
                        Editorial = "Alianza",
                        CantidadDePaginas = 671,
                        Precio = 24.00,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788420674209-L.jpg",
                        Sinopsis = "Un estudiante comete un asesinato y lidia con su culpa.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1866, 1, 1),
                        ListaAutores = new List<Autor> { Aut("Dostoievski") },
                        Categoria = Cat("Clásicos")
                    },
                    new Libro
                    {
                        Nombre = "Orgullo y prejuicio",
                        Isbn = "978-8420612409",
                        Editorial = "Alianza",
                        CantidadDePaginas = 424,
                        Precio = 17.50,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788420612409-L.jpg",
                        Sinopsis = "La historia de amor entre Elizabeth Bennet y el señor Darcy.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1813, 1, 28),
                        ListaAutores = new List<Autor> { Aut("Austen") },
                        Categoria = Cat("Romance")
                    },
                    new Libro
                    {
                        Nombre = "El código Da Vinci",
                        Isbn = "978-0385504201",
                        Editorial = "Planeta",
                        CantidadDePaginas = 601,
                        Precio = 21.00,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/0385504209-L.jpg",
                        Sinopsis = "Un simbologista descubre una conspiración religiosa milenaria.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(2003, 3, 18),
                        ListaAutores = new List<Autor> { Aut("Brown") },
                        Categoria = Cat("Misterio")
                    },
                    new Libro
                    {
                        Nombre = "Fahrenheit 451",
                        Isbn = "978-8445074824",
                        Editorial = "Minotauro",
                        CantidadDePaginas = 256,
                        Precio = 16.99,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788445074824-L.jpg",
                        Sinopsis = "En un futuro donde los libros están prohibidos, un bombero reflexiona.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1953, 10, 19),
                        ListaAutores = new List<Autor> { Aut("Bradbury") },
                        Categoria = Cat("Ciencia ficción")
                    },
                    new Libro
                    {
                        Nombre = "El alquimista",
                        Isbn = "978-8408052944",
                        Editorial = "Planeta",
                        CantidadDePaginas = 208,
                        Precio = 15.99,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788408052944-L.jpg",
                        Sinopsis = "Un pastor andaluz viaja en busca de un tesoro y su leyenda personal.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1988, 1, 1),
                        ListaAutores = new List<Autor> { Aut("Coelho") },
                        Categoria = Cat("Autoayuda")
                    },
                    new Libro
                    {
                        Nombre = "Dune",
                        Isbn = "978-8466338462",
                        Editorial = "Nova",
                        CantidadDePaginas = 896,
                        Precio = 27.99,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788466338462-L.jpg",
                        Sinopsis = "La historia de Paul Atreides en el desértico planeta Arrakis.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1965, 8, 1),
                        ListaAutores = new List<Autor> { Aut("Herbert") },
                        Categoria = Cat("Ciencia ficción")
                    },
                    new Libro
                    {
                        Nombre = "Sapiens",
                        Isbn = "978-8499926223",
                        Editorial = "Debate",
                        CantidadDePaginas = 496,
                        Precio = 23.50,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788499926223-L.jpg",
                        Sinopsis = "Una breve historia de la humanidad desde el Homo sapiens hasta hoy.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(2011, 1, 1),
                        ListaAutores = new List<Autor> { Aut("Harari") },
                        Categoria = Cat("Ciencia")
                    },
                    new Libro
                    {
                        Nombre = "El nombre de la rosa",
                        Isbn = "978-8432217654",
                        Editorial = "Lumen",
                        CantidadDePaginas = 544,
                        Precio = 20.00,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788432217654-L.jpg",
                        Sinopsis = "Un monje medieval investiga una serie de misteriosas muertes en una abadía.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1980, 1, 1),
                        ListaAutores = new List<Autor> { Aut("Eco") },
                        Categoria = Cat("Misterio")
                    },
                    new Libro
                    {
                        Nombre = "Anna Karenina",
                        Isbn = "978-8420674773",
                        Editorial = "Alianza",
                        CantidadDePaginas = 864,
                        Precio = 26.00,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788420674773-L.jpg",
                        Sinopsis = "La trágica historia de una aristócrata rusa y su amor prohibido.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1878, 1, 1),
                        ListaAutores = new List<Autor> { Aut("Tolstói") },
                        Categoria = Cat("Clásicos")
                    },
                    new Libro
                    {
                        Nombre = "Neuromante",
                        Isbn = "978-8466642828",
                        Editorial = "Minotauro",
                        CantidadDePaginas = 320,
                        Precio = 18.00,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788466642828-L.jpg",
                        Sinopsis = "Un hacker es contratado para realizar el mayor robo cibernético de la historia.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1984, 7, 1),
                        ListaAutores = new List<Autor> { Aut("Gibson") },
                        Categoria = Cat("Ciencia ficción")
                    },
                    new Libro
                    {
                        Nombre = "El hobbit",
                        Isbn = "978-8445000656",
                        Editorial = "Minotauro",
                        CantidadDePaginas = 304,
                        Precio = 19.50,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788445000656-L.jpg",
                        Sinopsis = "Bilbo Bolsón emprende una aventura con enanos y un mago.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1937, 9, 21),
                        ListaAutores = new List<Autor> { Aut("Tolkien") },
                        Categoria = Cat("Fantasía")
                    },
                    new Libro
                    {
                        Nombre = "Ficciones",
                        Isbn = "978-8420633138",
                        Editorial = "Alianza",
                        CantidadDePaginas = 224,
                        Precio = 14.99,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788420633138-L.jpg",
                        Sinopsis = "Colección de cuentos fantásticos del maestro argentino Borges.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1944, 1, 1),
                        ListaAutores = new List<Autor> { Aut("Borges") },
                        Categoria = Cat("Clásicos")
                    },
                    new Libro
                    {
                        Nombre = "La sombra del viento",
                        Isbn = "978-8408163435",
                        Editorial = "Planeta",
                        CantidadDePaginas = 576,
                        Precio = 22.00,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788408163435-L.jpg",
                        Sinopsis = "Un niño encuentra un libro misterioso en el Cementerio de los Libros Olvidados.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(2001, 1, 1),
                        ListaAutores = new List<Autor> { Aut("Zafón") },
                        Categoria = Cat("Misterio")
                    },
                    new Libro
                    {
                        Nombre = "Cosmos",
                        Isbn = "978-8408066217",
                        Editorial = "Planeta",
                        CantidadDePaginas = 368,
                        Precio = 21.50,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788408066217-L.jpg",
                        Sinopsis = "Carl Sagan nos lleva en un viaje por el universo y la historia de la ciencia.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1980, 1, 1),
                        ListaAutores = new List<Autor> { Aut("Sagan") },
                        Categoria = Cat("Ciencia")
                    },

                    // ── Batch 2 ──────────────────────────────────────────────────────────
                    new Libro
                    {
                        Nombre = "Matar a un ruiseñor",
                        Isbn = "978-8491051459",
                        Editorial = "HarperCollins",
                        CantidadDePaginas = 336,
                        Precio = 18.99,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788491051459-L.jpg",
                        Sinopsis = "Un abogado del sur de EE.UU. defiende a un hombre negro acusado injustamente.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1960, 7, 11),
                        ListaAutores = new List<Autor> { Aut("Lee") },
                        Categoria = Cat("Clásicos")
                    },
                    new Libro
                    {
                        Nombre = "El gran Gatsby",
                        Isbn = "978-8420674780",
                        Editorial = "Alianza",
                        CantidadDePaginas = 208,
                        Precio = 15.50,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788420674780-L.jpg",
                        Sinopsis = "La obsesión de un millonario por recuperar su amor perdido en los años 20.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1925, 4, 10),
                        ListaAutores = new List<Autor> { Aut("Fitzgerald") },
                        Categoria = Cat("Clásicos")
                    },
                    new Libro
                    {
                        Nombre = "Un mundo feliz",
                        Isbn = "978-8497930567",
                        Editorial = "Edhasa",
                        CantidadDePaginas = 311,
                        Precio = 17.99,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9780060850524-L.jpg",
                        Sinopsis = "Una sociedad futurista controlada por el placer y el condicionamiento genético.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1932, 1, 1),
                        ListaAutores = new List<Autor> { Aut("Huxley") },
                        Categoria = Cat("Ciencia ficción")
                    },
                    new Libro
                    {
                        Nombre = "Los miserables",
                        Isbn = "978-8420674803",
                        Editorial = "Alianza",
                        CantidadDePaginas = 1488,
                        Precio = 38.00,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9780451525260-L.jpg",
                        Sinopsis = "La redención de Jean Valjean en la Francia del siglo XIX.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1862, 1, 1),
                        ListaAutores = new List<Autor> { Aut("Hugo") },
                        Categoria = Cat("Clásicos")
                    },
                    new Libro
                    {
                        Nombre = "El proceso",
                        Isbn = "978-8420636054",
                        Editorial = "Alianza",
                        CantidadDePaginas = 272,
                        Precio = 16.50,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788420636054-L.jpg",
                        Sinopsis = "Josef K. es arrestado y juzgado sin conocer jamás su acusación.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1925, 1, 1),
                        ListaAutores = new List<Autor> { Aut("Kafka") },
                        Categoria = Cat("Clásicos")
                    },
                    new Libro
                    {
                        Nombre = "Fundación",
                        Isbn = "978-8466302029",
                        Editorial = "Nova",
                        CantidadDePaginas = 352,
                        Precio = 20.99,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788466302029-L.jpg",
                        Sinopsis = "Un matemático predice la caída del Imperio Galáctico e intenta preservar el conocimiento.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1951, 5, 1),
                        ListaAutores = new List<Autor> { Aut("Asimov") },
                        Categoria = Cat("Ciencia ficción")
                    },
                    new Libro
                    {
                        Nombre = "El retrato de Dorian Gray",
                        Isbn = "978-8420633817",
                        Editorial = "Alianza",
                        CantidadDePaginas = 288,
                        Precio = 15.99,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788420633817-L.jpg",
                        Sinopsis = "Un joven vende su alma para que un retrato envejezca en su lugar.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1890, 6, 20),
                        ListaAutores = new List<Autor> { Aut("Wilde") },
                        Categoria = Cat("Clásicos")
                    },
                    new Libro
                    {
                        Nombre = "Rayuela",
                        Isbn = "978-8437604572",
                        Editorial = "Cátedra",
                        CantidadDePaginas = 736,
                        Precio = 28.00,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788437604572-L.jpg",
                        Sinopsis = "Una novela experimental sobre un argentino errante en París y Buenos Aires.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1963, 6, 28),
                        ListaAutores = new List<Autor> { Aut("Cortázar") },
                        Categoria = Cat("Ficción")
                    },
                    new Libro
                    {
                        Nombre = "El túnel",
                        Isbn = "978-8423318278",
                        Editorial = "Seix Barral",
                        CantidadDePaginas = 192,
                        Precio = 14.50,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788423318278-L.jpg",
                        Sinopsis = "Un pintor obsesionado narra el crimen que cometió por amor.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1948, 1, 1),
                        ListaAutores = new List<Autor> { Aut("Sabato") },
                        Categoria = Cat("Misterio")
                    },
                    new Libro
                    {
                        Nombre = "Poeta en Nueva York",
                        Isbn = "978-8467032826",
                        Editorial = "Espasa",
                        CantidadDePaginas = 256,
                        Precio = 16.00,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788467032826-L.jpg",
                        Sinopsis = "Poemario de García Lorca inspirado en su estadía en Nueva York.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1940, 1, 1),
                        ListaAutores = new List<Autor> { Aut("Lorca") },
                        Categoria = Cat("Poesía")
                    },
                    new Libro
                    {
                        Nombre = "Siddhartha",
                        Isbn = "978-8475090009",
                        Editorial = "Edhasa",
                        CantidadDePaginas = 176,
                        Precio = 13.99,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788475090009-L.jpg",
                        Sinopsis = "El viaje espiritual de un joven indio en busca de la iluminación.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1922, 1, 1),
                        ListaAutores = new List<Autor> { Aut("Hesse") },
                        Categoria = Cat("Autoayuda")
                    },
                    new Libro
                    {
                        Nombre = "La metamorfosis",
                        Isbn = "978-8420637327",
                        Editorial = "Alianza",
                        CantidadDePaginas = 128,
                        Precio = 11.99,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788420637327-L.jpg",
                        Sinopsis = "Gregor Samsa amanece convertido en un insecto gigante.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1915, 10, 1),
                        ListaAutores = new List<Autor> { Aut("Kafka") },
                        Categoria = Cat("Clásicos")
                    },
                    new Libro
                    {
                        Nombre = "Ilíada",
                        Isbn = "978-8424914882",
                        Editorial = "Gredos",
                        CantidadDePaginas = 704,
                        Precio = 29.50,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788424914882-L.jpg",
                        Sinopsis = "El poema épico de Homero sobre la guerra de Troya.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1800, 1, 1),
                        ListaAutores = new List<Autor> { Aut("") }, // Homero
                        Categoria = Cat("Clásicos")
                    },
                    new Libro
                    {
                        Nombre = "El poder del ahora",
                        Isbn = "978-8484450955",
                        Editorial = "Gaia",
                        CantidadDePaginas = 256,
                        Precio = 17.00,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788484450955-L.jpg",
                        Sinopsis = "Una guía espiritual para vivir en el presente y alcanzar la paz interior.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1997, 1, 1),
                        ListaAutores = new List<Autor> { Aut("Tolle") },
                        Categoria = Cat("Autoayuda")
                    },
                    new Libro
                    {
                        Nombre = "Los hermanos Karamazov",
                        Isbn = "978-8420645155",
                        Editorial = "Alianza",
                        CantidadDePaginas = 1008,
                        Precio = 32.00,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788420645155-L.jpg",
                        Sinopsis = "Las tensiones filosóficas y familiares entre los hermanos Karamazov.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1880, 11, 1),
                        ListaAutores = new List<Autor> { Aut("Dostoievski") },
                        Categoria = Cat("Clásicos")
                    },
                    new Libro
                    {
                        Nombre = "El juego de Ender",
                        Isbn = "978-8490430798",
                        Editorial = "Nova",
                        CantidadDePaginas = 352,
                        Precio = 19.99,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9780812550702-L.jpg",
                        Sinopsis = "Un niño prodigio es entrenado para liderar la guerra contra una raza alienígena.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1985, 1, 15),
                        ListaAutores = new List<Autor> { Aut("Card") },
                        Categoria = Cat("Ciencia ficción")
                    },
                    new Libro
                    {
                        Nombre = "Crónicas marcianas",
                        Isbn = "978-8445074701",
                        Editorial = "Minotauro",
                        CantidadDePaginas = 288,
                        Precio = 17.50,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788445074701-L.jpg",
                        Sinopsis = "Una serie de relatos sobre la colonización humana del planeta Marte.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1950, 5, 4),
                        ListaAutores = new List<Autor> { Aut("Bradbury") },
                        Categoria = Cat("Ciencia ficción")
                    },
                    new Libro
                    {
                        Nombre = "Pedro Páramo",
                        Isbn = "978-8437500157",
                        Editorial = "Cátedra",
                        CantidadDePaginas = 160,
                        Precio = 13.50,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788437500157-L.jpg",
                        Sinopsis = "Un hombre viaja al pueblo de su padre muerto y encuentra solo voces y sombras.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1955, 3, 19),
                        ListaAutores = new List<Autor> { Aut("Rulfo") },
                        Categoria = Cat("Ficción")
                    },
                    new Libro
                    {
                        Nombre = "Confesiones",
                        Isbn = "978-8420682419",
                        Editorial = "Alianza",
                        CantidadDePaginas = 480,
                        Precio = 22.00,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788420682419-L.jpg",
                        Sinopsis = "La autobiografía espiritual de San Agustín y su conversión al cristianismo.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(397, 1, 1),
                        ListaAutores = new List<Autor> { Aut("Agustín") },
                        Categoria = Cat("Religión")
                    },
                    new Libro
                    {
                        Nombre = "Una breve historia del tiempo",
                        Isbn = "978-8408057949",
                        Editorial = "Planeta",
                        CantidadDePaginas = 240,
                        Precio = 18.50,
                        UrlImagenTapa = "https://covers.openlibrary.org/b/isbn/9788408057949-L.jpg",
                        Sinopsis = "Stephen Hawking explica el origen y destino del universo para el público general.",
                        Idioma = "Español",
                        FechaPublicacion = new DateTime(1988, 4, 1),
                        ListaAutores = new List<Autor> { Aut("Hawking") },
                        Categoria = Cat("Ciencia")
                    },
                };

                context.Libros.AddRange(libros);
                await context.SaveChangesAsync();
            }

            // 4) Rol admin y usuario administrador
            const string adminRoleName = "admin@libreria.local";
            const string adminEmail = "admin@libreria.local";
            const string adminPassword = "Admin123!";

            if (!await roleManager.RoleExistsAsync(adminRoleName))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRoleName));
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
                    await userManager.AddToRoleAsync(adminUser, adminRoleName);
            }
            else
            {
                if (!await userManager.IsInRoleAsync(adminUser, adminRoleName))
                    await userManager.AddToRoleAsync(adminUser, adminRoleName);
            }
        }
    }
}