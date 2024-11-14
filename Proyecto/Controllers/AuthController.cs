using Microsoft.AspNetCore.Mvc;

namespace Proyecto.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string nombre, string contra){
            var result = await _authService.LoginAsync(nombre, contra);
            if (!result)
            return Unauthorized("Correo o clave incorrecto");
            return
            View("~/Views/Home/index.cshtml");
        }
    }
}
