using CalculadoraMVC.Models;
using System.Threading.Tasks;

namespace CalculadoraMVC.Repository
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetUsuarioById(int id);
        Task<Usuario> GetUsuarioByEmailAndPassword(string email, string password);
        Task<bool> IsEmailTaken(string email);
        Task CreateUsuario(Usuario usuario);
        Task UpdateUsuario(Usuario usuario);
        Task DeleteUsuario(int id);
    }
}
