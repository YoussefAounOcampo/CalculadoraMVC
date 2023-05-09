using System.ComponentModel.DataAnnotations;

namespace CalculadoraMVC.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Email es requerido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo Contraseña es requerido.")]
        public string Password { get; set; }
    }

}
