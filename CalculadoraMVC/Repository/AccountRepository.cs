using CalculadoraMVC.Data;
using CalculadoraMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CalculadoraMVC.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> GetUsuarioById(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task UpdateUsuario(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }

}
