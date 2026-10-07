using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Biblioteca.Web.Models;
using Biblioteca.Web.Repositorios;

namespace Biblioteca.Web.Controllers
{
    public class LibrosController : Controller
    {
        private readonly LibroRepositorio _libroRepo;

        public LibrosController(LibroRepositorio libroRepo)
        {
            _libroRepo = libroRepo;
        }

        public async Task<IActionResult> Index(string buscar)
        {
            IEnumerable<Libro> libros;
            if (string.IsNullOrEmpty(buscar))
            {
                libros = await _libroRepo.ObtenerActivosAsync();
            }
            else
            {
                libros = await _libroRepo.BuscarPorTituloAsync(buscar);
                ViewData["Buscar"] = buscar;
            }
            return View(libros);
        }

        public async Task<IActionResult> Details(int id)
        {
            var libro = await _libroRepo.ObtenerPorIdAsync(id);
            if (libro == null) return NotFound();
            return View(libro);
        }

        public async Task<IActionResult> Create()
        {
            await CargarAutoresAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Libro libro)
        {
            if (ModelState.IsValid)
            {
                await _libroRepo.InsertarAsync(libro);
                TempData["Mensaje"] = "Libro creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            await CargarAutoresAsync();
            return View(libro);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var libro = await _libroRepo.ObtenerPorIdAsync(id);
            if (libro == null) return NotFound();
            await CargarAutoresAsync();
            return View(libro);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Libro libro)
        {
            if (ModelState.IsValid)
            {
                await _libroRepo.ActualizarAsync(libro);
                TempData["Mensaje"] = "Libro actualizado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            await CargarAutoresAsync();
            return View(libro);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var libro = await _libroRepo.ObtenerPorIdAsync(id);
            if (libro == null) return NotFound();
            return View(libro);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _libroRepo.EliminarAsync(id);
            TempData["Mensaje"] = "Libro eliminado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        private async Task CargarAutoresAsync()
        {
            var autores = await _libroRepo.ObtenerAutoresActivosAsync();
            ViewData["Autores"] = new SelectList(autores, "AutorId", "Nombre");
        }
    }
}
