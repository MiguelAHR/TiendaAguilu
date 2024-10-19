namespace Proyecto.Models
{
    public class Kardex
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public int TipoMovId { get; set; }
        public TipoMovimiento TipoMovimiento { get; set; }
        public int InventarioId { get; set; }
        public Inventario Inventario { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int jefeAlmacenId { get; set; }
        public JefeAlmacen JefeAlmacen { get; set; }
    }
}
