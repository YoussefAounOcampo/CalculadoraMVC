using CalculadoraMVC.Models;

namespace CalculadoraMVC.Repository
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetUsuarioById(int id);
        Task<Usuario> GetUsuarioByEmail(string email);
        Task CreateUsuario(Usuario usuario);
        Task UpdateUsuario(Usuario usuario);
        Task DeleteUsuario(int id);
    }
}
