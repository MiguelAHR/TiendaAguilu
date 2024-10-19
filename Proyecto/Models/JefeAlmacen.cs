namespace Proyecto.Models
{
    public class JefeAlmacen
    {
        public JefeAlmacen() { Kardexs = new HashSet<Kardex>(); }
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public string Credencial { get; set; }
        public IEnumerable<Kardex> Kardexs { get; set; }
    }
}
