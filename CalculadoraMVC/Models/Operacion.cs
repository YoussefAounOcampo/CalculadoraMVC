using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculadoraMVC.Models
{

    public class Operacion
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string OperacionString { get; set; }
        [MaxLength(100)]
        [Required]
        public string Resultado { get; set; }
        public DateTime Fecha { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
    }


}
