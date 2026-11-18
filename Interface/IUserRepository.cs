using CIt_Demo_Project.DTO;
using CIt_Demo_Project.Model;

namespace CIt_Demo_Project.Interface
{
    public interface IUserRepository
    {
        Task<bool> usernameexistsAsync(string username);
        Task<bool> emailexistsAsync(string email);
        Task<User?> GetbyEmailAsync(string email);
        Task AddAsync(User users);
    }
}
