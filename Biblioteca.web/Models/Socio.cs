using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Web.Models
{
    public class Socio
    {
        public int SocioId { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio.")]
        [StringLength(20, ErrorMessage = "El DNI no puede exceder los 20 caracteres.")]
        public string DNI { get; set; } = null!;

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
        [StringLength(100, ErrorMessage = "El email no puede exceder los 100 caracteres.")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public bool Activo { get; set; }
    }
}
