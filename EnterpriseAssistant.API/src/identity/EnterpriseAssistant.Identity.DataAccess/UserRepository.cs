using System;
using System.Data;
using System.Threading.Tasks;
using EnterpriseAssistant.Identity.DataAccess.Entities;

namespace EnterpriseAssistant.Identity.DataAccess
{
    internal class UserRepository : IUserRepository
    {
        private readonly IDbConnection _connection;

        public UserRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        // todo: replace by valid logic
        public async Task<User> GetUserByLogin(string login)
        {
            throw new NotImplementedException();
        }

        public async Task<User> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}