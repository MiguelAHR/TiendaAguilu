using System.ComponentModel.DataAnnotations;

namespace Proyecto.Models
{
    public class Proveedor
    {
        public Proveedor() { 
            Productos = new HashSet<Producto>();
            Pedidos = new HashSet<Pedido>();
        }
        public int Id { get; set; }
        [StringLength(100)]
        public string Nombre { get; set; }
        [StringLength(20)]
        public string Telefono { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        public IEnumerable<Producto> Productos { get; set; }
        public IEnumerable<Pedido> Pedidos { get; set; }
    }
}
