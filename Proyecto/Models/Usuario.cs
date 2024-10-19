namespace Proyecto.Models
{
    public class Usuario
    {
        //TABLA PADRE - ORIGEN (parte 1 - primary)
        public Usuario() {
            AuxiliarAlmacens = new HashSet<AuxiliarAlmacen>();
            JefeAlmacens = new HashSet<JefeAlmacen>();
            Administradors = new HashSet<Administrador>();
        }
        //
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Contraseña { get; set; }
        public int RolId { get; set; }//CONECTA CON OTRA TABLA (parte 1 - foranea)
        public Rol Rol { get; set; }//CONECTA CON OTRA TABLA (parte 2 - foranea)
        public int PersonaId { get; set; }
        public Persona Persona { get; set; }

        //TABLA PADRE - ORIGEN (parte 2 - primary)
        public IEnumerable<AuxiliarAlmacen> AuxiliarAlmacens { get; set; }
        public IEnumerable<JefeAlmacen> JefeAlmacens { get; set; }
        public IEnumerable<Administrador> Administradors { get; set; }
        //
    }
}
