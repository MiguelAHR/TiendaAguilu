using System.ComponentModel.DataAnnotations;

namespace Proyecto.Models
{
    public class TipoPago
    {
        public TipoPago() { RegistroPagos = new HashSet<RegistroPago>(); }
        public int Id { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; }
        [StringLength(255)]
        public string? Descripcion { get; set; }
        public IEnumerable<RegistroPago> RegistroPagos { get; set; }
    }
}
