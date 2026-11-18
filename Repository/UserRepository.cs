using CIt_Demo_Project.Data;
using CIt_Demo_Project.Interface;
using CIt_Demo_Project.Model;
using Microsoft.EntityFrameworkCore;

namespace CIt_Demo_Project.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext appDb;

        public UserRepository(AppDbContext dbContext)
        {
            appDb = dbContext;
        }
        public async Task AddAsync(User users)
        {
            await appDb.users.AddAsync(users);
            await appDb.SaveChangesAsync();
        }

        public async Task<bool> emailexistsAsync(string email)
        {
            return await appDb.users.AnyAsync(u => u.email == email);
        }

        public async Task<User?> GetbyEmailAsync(string email)
        {
            return await appDb.users.FirstOrDefaultAsync(u => u.email == email);
        }

        public async Task<bool> usernameexistsAsync(string username)
        {
            return await appDb.users.AnyAsync(u => u.username == username);
        }
    }
}
