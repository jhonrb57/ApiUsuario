using System.ComponentModel.DataAnnotations;

namespace ApiUsuario.Models
{
    public class Usuario
    {
        [Key]
        public string Id { get; set; }
        public string Nombre { get; set; }
        public int Cedula { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
    }
}
