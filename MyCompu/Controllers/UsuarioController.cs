using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCompu.Data;
using MyCompu.Models;

namespace MyCompu.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly MyCompuDbContext _context;

        public UsuarioController(MyCompuDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            return View(usuario);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string correo, string contraseña)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo && u.Contraseña == contraseña);

            if (usuario != null)
            {
                HomeController.UsuarioGlobal=usuario;
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Correo o contraseña incorrectos.");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MisProductos()
        {
            var usuario = HomeController.UsuarioGlobal;

            if (usuario == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            // Asegúrate de que los productos estén cargados si es necesario
            usuario = await _context.Usuarios
                        .Include(u => u.Productos)
                        .FirstOrDefaultAsync(u => u.Id == usuario.Id);

            return View(usuario.Productos);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Lógica para cerrar sesión
            HomeController.UsuarioGlobal = null;
            return RedirectToAction("Index", "Home");
        }
    }
}
