using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Web.Models
{
    public class PrestamoReporte
    {
        [Display(Name = "Socio")]
        public string SocioNombre { get; set; } = null!;

        [Display(Name = "Libro")]
        public string LibroTitulo { get; set; } = null!;

        [Display(Name = "Fecha Límite")]
        [DataType(DataType.Date)]
        public DateTime FechaLimite { get; set; }

        public string Estado { get; set; } = null!;
    }
}
