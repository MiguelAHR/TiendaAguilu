namespace Proyecto.Models
{
    public class Administrador
    {
        public Administrador()
        {
            Pedidos = new HashSet<Pedido>();
            RegistroPagos = new HashSet<RegistroPago>();
        }
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public string Credencial { get; set; }
        public IEnumerable<Pedido> Pedidos { get; set; }
        public IEnumerable<RegistroPago> RegistroPagos { get; set; }
    }
}
