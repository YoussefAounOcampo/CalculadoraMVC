using CalculadoraMVC.Data;
using CalculadoraMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CalculadoraMVC.Repository;
public class AccountController : Controller
{
    private readonly IAccountRepository _accountRepository;

    public AccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<IActionResult> Cuenta()
    {
        var id = CurrentUser.Id;

        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _accountRepository.GetUsuarioById(id.Value);

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

        var usuario = await _accountRepository.GetUsuarioById(id.Value);

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

        var usuario = await _accountRepository.GetUsuarioById(id.Value);

        if (usuario == null)
        {
            return NotFound();
        }

        usuario.Email = model.Email;
        usuario.Password = model.Password;

        await _accountRepository.UpdateUsuario(usuario);

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

        var usuario = await _accountRepository.GetUsuarioById(id.Value);

        if (usuario == null)
        {
            return NotFound();
        }

        await _accountRepository.DeleteUsuario(usuario.Id);

        CurrentUser.Id = null;

        return RedirectToAction("Index", "Calculadora");
    }
}