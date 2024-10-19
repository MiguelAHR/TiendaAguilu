namespace Proyecto.Models
{
    public class Pedido
    {
        public Pedido()
        {
            DetallePedidos = new HashSet<DetallePedido>();
            RegistroPagos = new HashSet<RegistroPago>();
        }
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public int ProveedorId { get; set; }
        public int ProductoId { get; set; }
        public int AdminId { get; set; }
        public Proveedor Proveedor { get; set; }
        public Producto Producto { get; set; }
        public Administrador Administrador { get; set; }
        public IEnumerable<DetallePedido> DetallePedidos { get; set; }
        public IEnumerable<RegistroPago> RegistroPagos { get; set; }
    }
}
