using CalculadoraMVC.Data;
using CalculadoraMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CalculadoraMVC.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegisterController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Usuario model)
        {
            // Verificar si ya existe un usuario con el mismo correo electrónico
            var existingUser = _context.Usuarios.FirstOrDefault(u => u.Email == model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "Ya existe un usuario registrado con ese correo electrónico.");
                return View(model);
            }
            
            if (ModelState.IsValid)
            {
                _context.Usuarios.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index", "Login");
            }

            return View(model);
        }


    }
}
