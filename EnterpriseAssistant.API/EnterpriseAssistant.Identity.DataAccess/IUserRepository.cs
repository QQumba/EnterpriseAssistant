using System.Threading.Tasks;
using EnterpriseAssistant.Identity.DataAccess.Entities;

namespace EnterpriseAssistant.Identity.DataAccess
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<User> GetUserByLogin(string login);
        Task<User> UpdateUser(User user);
    }
}