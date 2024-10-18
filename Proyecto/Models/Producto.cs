using System.ComponentModel.DataAnnotations;

namespace Proyecto.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo de el nombre debe ser obligatorio")]
        public string Nombre { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public string? Descripcion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public int Cantidad { get; set; }
        public int? CategoriaId { get; set; }
        public int? ProveedorId { get; set; }
        public Categoria Categoria { get; set; }
        public Proveedor Proveedor { get; set; }
    }
}
