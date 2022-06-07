using System.Threading.Tasks;
using EnterpriseAssistant.Identity.DataAccess.Entities;

namespace EnterpriseAssistant.Identity.DataAccess
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<User> GetUserByEmail(string email);
    }
}