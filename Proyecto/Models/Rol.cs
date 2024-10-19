using System.ComponentModel.DataAnnotations;

namespace Proyecto.Models
{
    public class Rol
    {
        public Rol() { Usuarios = new HashSet<Usuario>(); }
        public int Id { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; }
        [StringLength(255)]
        public string? Descripcion { get; set; }
        public IEnumerable<Usuario> Usuarios { get; set; }
    }
}
