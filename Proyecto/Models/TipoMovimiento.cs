using System.ComponentModel.DataAnnotations;

namespace Proyecto.Models
{
    public class TipoMovimiento
    {
        public TipoMovimiento() { Kardexs = new HashSet<Kardex>(); }
        public int Id { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; }
        [StringLength(255)]
        public string? Descripcion { get; set; }
        public IEnumerable<Kardex> Kardexs { get; set; }
    }
}
