using CalculadoraMVC.Models;

namespace CalculadoraMVC.Repository
{
    public interface IAccountRepository
    {
        Task<Usuario> GetUsuarioById(int id);
        Task UpdateUsuario(Usuario usuario);
        Task DeleteUsuario(int id);
    }
}
