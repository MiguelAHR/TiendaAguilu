using Microsoft.AspNetCore.Mvc;
using Proyecto.Data;
using BCrypt.Net;

namespace Proyecto.Controllers
{
    public class AuthService : Controller
    {
        private readonly ProyectoContext _proyectoContext;

        public AuthService(ProyectoContext proyectoContext)
        {
            _proyectoContext = proyectoContext;
        }

        public async Task<bool> LoginAsync(string nombre, string contra){
            var usuario = _proyectoContext.Usuarios.FirstOrDefault(u => u.Nombre == nombre);

            if (usuario == null) { return false; }
            if (!BCrypt.Net.BCrypt.Verify(contra, usuario.Contraseña)) { return false; }

            //usuario.Sesion = DateTime.Now; <-- no tenemos
            _proyectoContext.SaveChanges();
            return true;
        }
    }
}
