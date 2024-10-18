using System.ComponentModel.DataAnnotations;

namespace Proyecto.Models
{
    public class Categoria
    {
        public Categoria() { Productos = new HashSet<Producto>(); }
        public int Id { get; set; }
        [StringLength(100)]
        public string Nombre { get; set; }
        public  IEnumerable<Producto> Productos { get; set; }
    }
}
