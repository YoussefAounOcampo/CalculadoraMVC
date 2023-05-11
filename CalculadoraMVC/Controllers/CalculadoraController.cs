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
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IOperacionRepository _operacionRepository;

    public CalculadoraController(IUsuarioRepository usuarioRepository, IOperacionRepository operacionRepository)
    {
        _usuarioRepository = usuarioRepository;
        _operacionRepository = operacionRepository;
    }

    public IActionResult Index(int id)
    {
        CurrentUser.Id= id;
        return View();
    }


    public async Task<IActionResult> Privacy()
    {
        var id = CurrentUser.Id;

        if (id == null)
        {
            return NotFound();
        }

        var operaciones = await _operacionRepository.GetOperacionesByUsuarioId(id.Value);

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
            await _operacionRepository.CreateOperacion(operacion);
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
        await _operacionRepository.DeleteOperacion(id);

        return RedirectToAction(nameof(Privacy));
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
