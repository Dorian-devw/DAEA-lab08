using Microsoft.AspNetCore.Mvc;
using Biblioteca.Web.Models;
using Biblioteca.Web.Repositorios;

namespace Biblioteca.Web.Controllers
{
    public class SociosController : Controller
    {
        private readonly SocioRepositorio _socioRepo;

        public SociosController(SocioRepositorio socioRepo)
        {
            _socioRepo = socioRepo;
        }

        public async Task<IActionResult> Index()
        {
            var socios = await _socioRepo.ObtenerActivosAsync();
            return View(socios);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Socio socio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _socioRepo.InsertarAsync(socio);
                    TempData["Mensaje"] = "Socio creado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Microsoft.Data.SqlClient.SqlException ex)
                {
                    if (ex.Number == 2627 || ex.Number == 2601)
                    {
                        ModelState.AddModelError("DNI", "Ya existe un socio con ese DNI.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Ocurrió un error al guardar la información.");
                    }
                }
            }
            return View(socio);
        }
    }
}
