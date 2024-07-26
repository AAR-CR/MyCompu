using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCompu.Data;
using MyCompu.Models;
using System.Diagnostics;

namespace MyCompu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public static Usuario? UsuarioGlobal;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
           
            return View();
        }

        //public IActionResult ListaProductos()
        //{
        //    var productos = ProductoData.Productos; return View(productos);
        //}

        //[HttpGet]
        //public IActionResult CrearUsuario()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult CrearUsuario(Usuario usuario)
        //{
        //    if (usuario.Correo !=null && usuario.Contraseña !=null)
        //    {
        //        // Generar un ID ficticio para el usuario
        //        usuario.Id = new System.Random().Next(1, 1000);
                
        //        UsuarioGlobal = usuario;

        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
