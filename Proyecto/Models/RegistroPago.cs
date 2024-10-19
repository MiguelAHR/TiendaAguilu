namespace Proyecto.Models
{
    public class RegistroPago
    {
        public int Id { get; set; }
        public string? Detalle { get; set; }
        public string Estado { get; set; }
        public int TipoPagoId { get; set; }
        public TipoPago TipoPago { get; set; }
        public int AdminId { get; set; }
        public Administrador Administrador { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
    }
}
