using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCompu.Data;
using MyCompu.Models;

namespace MyCompu.Controllers
{
    public class ProductoController : Controller
    {
        private readonly MyCompuDbContext _context;
        private static List<Producto> _carrito = new List<Producto>(); // Inicializar el carrito
        public ProductoController(MyCompuDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Productos.ToListAsync());
        }

        public async Task<IActionResult> Compras(int? id)         //CARRITO DE COMPRAS
        {
            if (HomeController.UsuarioGlobal == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                if (id.HasValue)
                {
                    var producto = await _context.Productos.FindAsync(id.Value);
                    if (producto != null)
                    {
                        _carrito.Add(producto);
                    }
                    else
                    {
                        return NotFound();
                    }
                }

                // Devolver la vista con el carrito, ya sea que se haya añadido un producto o no
                return View(_carrito);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Confirmar() // 
        {
            if (HomeController.UsuarioGlobal == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            // Cargar el usuario desde la base de datos
            var usuario = await _context.Usuarios
                                        .Include(u => u.Productos)
                                        .FirstOrDefaultAsync(u => u.Id == HomeController.UsuarioGlobal.Id);

            if (usuario == null)
            {
                return NotFound();
            }

            // Añadir los productos del carrito a la lista de productos del usuario
            foreach (var producto in _carrito)
            {
                usuario.Productos.Add(producto);
                // Opcional: actualizar el producto en la base de datos si es necesario
                // _context.Productos.Update(producto);
            }

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Vaciar el carrito después de la compra
            _carrito.Clear();

            // Actualizar UsuarioGlobal para reflejar los cambios
            HomeController.UsuarioGlobal = usuario;

            return View(); // Vista de confirmación
        }

    }
}