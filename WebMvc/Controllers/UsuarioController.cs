using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebMvc.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: UsuarioController
        public async Task<IActionResult> Login()
        {
            return View();
        }
        public async Task<IActionResult> SignUp()
        {
            return View();
        }
    }
}
