using CalculadoraMVC.Models;

namespace CalculadoraMVC.Repository
{
    public interface ILoginRepository
    {
        Task<Usuario> GetUsuarioByEmailAndPasswordAsync(string email, string password);

    }
}
