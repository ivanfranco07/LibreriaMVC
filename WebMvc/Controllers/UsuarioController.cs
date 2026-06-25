using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebMvc.Models;

namespace WebMvc.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public UsuarioController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: UsuarioController
        public async Task<IActionResult> LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                //PasswordSignInAsync busca por UserName, no por Email.
                var resultado = await _signInManager.PasswordSignInAsync(usuario.Email, usuario.Clave, usuario.Recordarme, lockoutOnFailure: false);
                
                if (resultado.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Inicio de sesión inválido.");
                }
            }
            return View(usuario);

        }
        public async Task<IActionResult> SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(RegistroViewModel usuarioRegistrado)
        {
            if (ModelState.IsValid)
            {
                var usuario = new Usuario
                {
                    UserName = usuarioRegistrado.Email,
                    Email = usuarioRegistrado.Email,
                    Nombre = usuarioRegistrado.Nombre,
                    Apellido = usuarioRegistrado.Apellido
                };
                var resultado = await _userManager.CreateAsync(usuario, usuarioRegistrado.Clave);
                if (resultado.Succeeded)
                {
                    await _signInManager.SignInAsync(usuario, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in resultado.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(usuarioRegistrado);
        }
        public IActionResult SignOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        
        public IActionResult Profile()
        {
            var usuarioActual = _userManager.GetUserAsync(User).Result;

            if (usuarioActual == null)
            {
                return RedirectToAction("LogIn");
            }

            var model = new ProfileViewModel
            {
                Nombre = usuarioActual.Nombre,
                Apellido = usuarioActual.Apellido,
                Email = usuarioActual.Email,
                FechaNacimiento = usuarioActual.FechaNacimiento
            };

            return View(model);
        }
    }
}