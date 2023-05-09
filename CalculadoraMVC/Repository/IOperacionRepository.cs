using CalculadoraMVC.Models;
using System.Collections.Generic;

public interface IOperacionRepository
{
    Operacion GetOperacionById(int id);
    IEnumerable<Operacion> GetOperacionesByUsuarioId(int usuarioId);
    void CreateOperacion(Operacion operacion);
    void UpdateOperacion(Operacion operacion);
    void DeleteOperacion(int id);
}
