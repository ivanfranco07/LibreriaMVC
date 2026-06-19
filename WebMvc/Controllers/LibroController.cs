using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMvc.Data;
using WebMvc.Models;

namespace WebMvc.Controllers
{
    public class LibroController : Controller
    {
        private readonly LibreriaDbContext _context;

        public LibroController(LibreriaDbContext context)
        {
            _context = context;
        }

        // GET: LibroController
        public async Task<IActionResult> Index()
        {
            var libros = _context.Libros.Include(l => l.Categoria).Include(l => l.ListaAutores);

            return View(await libros.ToListAsync());
        }

        // GET: LibroController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var libro = await _context.Libros.Include(l => l.ListaAutores).Include(l => l.Categoria)
                .FirstOrDefaultAsync(l => l.Id == id);

            return View(libro);
        }

        // GET: LibroController/Create
        public ActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Descripcion");

            return View();
        }

        // POST: LibroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Libro libro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Descripcion", libro.Id);
            return View(libro);
        }

        // GET: LibroController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (!ExisteLibro(id))
            {
                return NotFound();
            }

            var libro = await _context.Libros.Include(l => l.ListaAutores).Include(l => l.Categoria)
                .FirstOrDefaultAsync(l => l.Id == id);

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion", libro.Id);

            return View(libro);
        }

        // POST: LibroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Libro libro)
        {
            if (!ExisteLibro(libro.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExisteLibro(libro.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion", libro.CategoriaId);
            return View(libro);
        }

        // GET: LibroController/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            var libro = await _context.Libros.Include(l => l.ListaAutores).Include(l => l.Categoria)
                .FirstOrDefaultAsync(l => l.Id == id);

            return View(libro);
        }

        // POST: LibroController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libro = await _context.Libros.FindAsync(id);

            if (libro != null)
            {
                _context.Libros.Remove(libro);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public bool ExisteLibro(int id)
        {
            return _context.Libros.Any(l => l.Id == id);
        }
    }
}
