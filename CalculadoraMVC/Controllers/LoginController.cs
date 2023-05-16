using CalculadoraMVC.Data;
using CalculadoraMVC.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using CalculadoraMVC.Repository;

public class LoginController : Controller
{
    private readonly ILoginRepository _loginRepository;

    public LoginController(ILoginRepository loginRepository)
    {
        _loginRepository = loginRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(Usuario model)
    {
        var usuario = await _loginRepository.GetUsuarioByEmailAndPasswordAsync(model.Email, model.Password);

        if (usuario != null)
        {
            CurrentUser.Id=usuario.Id;
            return Redirect("/Calculadora/Index");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos.");
            return View("Index", model);
        }
    }

    public IActionResult Logout()
    {
        CurrentUser.Id = 0;
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Login");
    }

}
