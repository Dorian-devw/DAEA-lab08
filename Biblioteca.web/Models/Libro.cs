using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Web.Models
{
    public class Libro
    {
        public int LibroId { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(200, ErrorMessage = "El título no puede exceder los 200 caracteres.")]
        [Display(Name = "Título")]
        public string Titulo { get; set; } = null!;

        [Required(ErrorMessage = "El ISBN es obligatorio.")]
        [StringLength(50, ErrorMessage = "El ISBN no puede exceder los 50 caracteres.")]
        public string ISBN { get; set; } = null!;

        [Required(ErrorMessage = "El autor es obligatorio.")]
        [Display(Name = "Autor")]
        public int AutorId { get; set; }

        [Required(ErrorMessage = "El número de ejemplares es obligatorio.")]
        [Range(0, 1000, ErrorMessage = "Los ejemplares deben estar entre 0 y 1000.")]
        public int Ejemplares { get; set; }

        public bool Activo { get; set; }

        [Display(Name = "Autor")]
        public string? AutorNombre { get; set; }
    }
}
