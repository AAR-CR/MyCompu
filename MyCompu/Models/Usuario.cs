using System.ComponentModel.DataAnnotations;

namespace MyCompu.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string Contraseña { get; set; }

        public List<Producto>? Productos { get; set; }
       
        
    }
}
