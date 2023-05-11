using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalculadoraMVC.Data;
using CalculadoraMVC.Models;
using Microsoft.EntityFrameworkCore;

public class OperacionRepository : IOperacionRepository
{
    private readonly ApplicationDbContext _context;

    public OperacionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Operacion> GetOperacionById(int id)
    {
        return await _context.Operaciones.FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Operacion>> GetOperacionesByUsuarioId(int usuarioId)
    {
        return await _context.Operaciones.Where(o => o.IdUsuario == usuarioId).ToListAsync();
    }

    public async Task CreateOperacion(Operacion operacion)
    {
        if (operacion == null)
        {
            throw new ArgumentNullException(nameof(operacion));
        }

        await _context.Operaciones.AddAsync(operacion);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateOperacion(Operacion operacion)
    {
        if (operacion == null)
        {
            throw new ArgumentNullException(nameof(operacion));
        }

        _context.Entry(operacion).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOperacion(int id)
    {
        var operacion = await _context.Operaciones.FindAsync(id);

        if (operacion != null)
        {
            _context.Operaciones.Remove(operacion);
            await _context.SaveChangesAsync();
        }
    }
}
