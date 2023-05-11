using CalculadoraMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IOperacionRepository
{
    Task<Operacion> GetOperacionById(int id);
    Task<IEnumerable<Operacion>> GetOperacionesByUsuarioId(int usuarioId);
    Task CreateOperacion(Operacion operacion);
    Task UpdateOperacion(Operacion operacion);
    Task DeleteOperacion(int id);
}
