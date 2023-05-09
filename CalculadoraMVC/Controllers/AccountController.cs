using CalculadoraMVC.Data;
using CalculadoraMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CalculadoraMVC.Repository;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IUsuarioRepository _usuarioRepository;

    public AccountController(ApplicationDbContext context, IUsuarioRepository usuarioRepository)
    {
        _context = context;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IActionResult> Cuenta()
    {
        var id = CurrentUser.Id;

        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _usuarioRepository.GetUsuarioById(id.Value);

        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    public async Task<IActionResult> Editar()
    {
        var id = CurrentUser.Id;

        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _usuarioRepository.GetUsuarioById(id.Value);

        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(Usuario model)
    {
        var id = CurrentUser.Id;

        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _usuarioRepository.GetUsuarioById(id.Value);

        if (usuario == null)
        {
            return NotFound();
        }

        usuario.Email = model.Email;
        usuario.Password = model.Password;

        await _usuarioRepository.UpdateUsuario(usuario);

        return RedirectToAction(nameof(Cuenta));
    }

    [HttpPost]
    public async Task<IActionResult> Eliminar()
    {
        var id = CurrentUser.Id;

        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _usuarioRepository.GetUsuarioById(id.Value);

        if (usuario == null)
        {
            return NotFound();
        }

        await _usuarioRepository.DeleteUsuario(usuario.Id);

        CurrentUser.Id = null;

        return RedirectToAction("Index", "Calculadora");
    }
}
