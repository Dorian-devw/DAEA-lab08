using Microsoft.AspNetCore.Mvc;
using Biblioteca.Web.Repositorios;

namespace Biblioteca.Web.Controllers
{
    public class PrestamosController : Controller
    {
        private readonly SocioRepositorio _socioRepo;

        public PrestamosController(SocioRepositorio socioRepo)
        {
            _socioRepo = socioRepo;
        }

        public async Task<IActionResult> Reporte(DateTime? desde, DateTime? hasta)
        {
            DateTime fechaDesde = desde ?? DateTime.Now.AddDays(-30);
            DateTime fechaHasta = hasta ?? DateTime.Now;

            ViewData["Desde"] = fechaDesde.ToString("yyyy-MM-dd");
            ViewData["Hasta"] = fechaHasta.ToString("yyyy-MM-dd");

            var reporte = await _socioRepo.ReportePrestamosAsync(fechaDesde, fechaHasta);
            return View(reporte);
        }
    }
}
