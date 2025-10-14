using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ExistsByNameAsync(string nombre);
        Task<Usuario> GetByNameAsync(string nombre);
        Task<Usuario> GetByIdAsync(int id);
        Task AddAsync(Usuario usuario);
    }
}
