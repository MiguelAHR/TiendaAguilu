using System.ComponentModel.DataAnnotations;

namespace Proyecto.Models
{
    public class Producto
    {
        public Producto() { 
            Kardexs = new HashSet<Kardex>();
            Pedidos = new HashSet<Pedido>();
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre es Obligatorio")]
        public string Nombre { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int Cantidad { get; set; }
        public int CategoriaId { get; set; }
        public int ProveedorId { get; set; }
        public Categoria Categoria { get; set; }
        public Proveedor Proveedor { get; set; }
        public IEnumerable<Kardex> Kardexs { get; set; }
        public IEnumerable<Pedido> Pedidos { get; set; }
    }
}
