using CalculadoraMVC.Data;
using CalculadoraMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using CalculadoraMVC.Repository;

public static class CurrentUser
{
   
    public static int? Id { get; set; } = 0;
}

public class CalculadoraController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IUsuarioRepository _usuarioRepository;


    public CalculadoraController(ApplicationDbContext context ,IUsuarioRepository usuarioRepository)
    {
        _context = context;
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Index(int id)
    {
        CurrentUser.Id= id;
        return View();
    }


    public IActionResult Privacy()
    {
        var id = CurrentUser.Id;

        if (id == null)
        {
            return NotFound();
        }

        var operaciones = _context.Operaciones.Where(b => b.IdUsuario == id).ToList();

        return View(operaciones);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterOperation(string operationString, string resultado)
    {
        var id = CurrentUser.Id;

        if (id == null)
        {
            return NotFound();
        }

        var operacion = new Operacion
        {
            IdUsuario = id.Value,
            OperacionString = operationString,
            Resultado = resultado,
            Fecha = DateTime.Now
        };

        try
        {
            _context.Operaciones.Add(operacion);
            await _context.SaveChangesAsync();
            return Ok("Operación registrada en la base de datos.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);

            return BadRequest($"Error al registrar la operación en la base de datos: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var operacion = await _context.Operaciones.FindAsync(id);

        if (operacion == null)
        {
            return NotFound();
        }

        _context.Operaciones.Remove(operacion);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Privacy));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
