using System;
using System.Collections.Generic;
using System.Linq;
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

    public Operacion GetOperacionById(int id)
    {
        return _context.Operaciones.FirstOrDefault(o => o.Id == id);
    }

    public IEnumerable<Operacion> GetOperacionesByUsuarioId(int usuarioId)
    {
        return _context.Operaciones.Where(o => o.IdUsuario == usuarioId).ToList();
    }

    public void CreateOperacion(Operacion operacion)
    {
        if (operacion == null)
        {
            throw new ArgumentNullException(nameof(operacion));
        }

        _context.Operaciones.Add(operacion);
        _context.SaveChanges();
    }

    public void UpdateOperacion(Operacion operacion)
    {
        if (operacion == null)
        {
            throw new ArgumentNullException(nameof(operacion));
        }

        _context.Entry(operacion).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void DeleteOperacion(int id)
    {
        var operacion = _context.Operaciones.Find(id);

        if (operacion != null)
        {
            _context.Operaciones.Remove(operacion);
            _context.SaveChanges();
        }
    }
}
