using System.ComponentModel.DataAnnotations;

namespace Proyecto.Models
{
    public class Inventario
    {
        public Inventario() { Kardexs = new HashSet<Kardex>(); }
        public int Id { get; set; }
        public int CantidadMinima { get; set; }
        public int CantidadMaxima { get; set; }
        public DateTime fechaIngreso { get; set; }
        public int Capacidad { get; set; }
        [StringLength(50)]
        public string Estado { get; set; }
        public IEnumerable<Kardex> Kardexs { get; set; }
    }
}
