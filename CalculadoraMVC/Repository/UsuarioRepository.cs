using CalculadoraMVC.Data;
using CalculadoraMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CalculadoraMVC.Repository
{
    public class UsuarioRepository : IUsuarioRepository, ILoginRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> GetUsuarioById(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task CreateUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
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

        public async Task<Usuario> GetUsuarioByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<Usuario> GetUsuarioByEmailAndPassword(string email, string password)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<bool> IsEmailTaken(string email)
        {
            return await _context.Usuarios.AnyAsync(u => u.Email == email);
        }
    }
}
