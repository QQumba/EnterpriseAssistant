using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnterpriseAssistant.Identity.DataAccess.Entities;
using EnterpriseAssistant.Identity.DataAccess.Security;

namespace EnterpriseAssistant.Identity.DataAccess
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users = new();

        public InMemoryUserRepository()
        {
            var secret = UserSecret.Create("qwe");
            var user = new User
            {
                Id = 1,
                Email = "test@mail.com",
                FirstName = "test user",
                Salt = secret.Salt,
                Password = secret.PasswordHash,
                IsSoftDeleted = false
            };

            _users.Add(user);
        }

        public Task<User> CreateUserAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetUserByEmail(string email)
        {
            return Task.FromResult(_users.FirstOrDefault(u => u.Email.Equals(email)));
        }
    }
}