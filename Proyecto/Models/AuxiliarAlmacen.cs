namespace Proyecto.Models
{
    public class AuxiliarAlmacen
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public string Credencial { get; set; }
    }
}
