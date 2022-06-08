using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using EnterpriseAssistant.Identity.DataAccess.Entities;
using EnterpriseAssistant.Identity.DataAccess.Helpers;

namespace EnterpriseAssistant.Identity.DataAccess
{
    internal class UserRepository : IUserRepository
    {
        private readonly IDbConnection _connection;

        public UserRepository(IDbConnection connection)
        {
            _connection = connection;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var now = DateTime.UtcNow;
            user.CreatedAt = now;
            user.UpdatedAt = now;

            var query = SqlHelper.ReadSql("user_create");
            var createdUser = await _connection.QueryFirstOrDefaultAsync<User>(query, user);
            return createdUser;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var query = SqlHelper.ReadSql("user_read");
            var user = await _connection.QueryFirstOrDefaultAsync<User>(query, new {Email = email});
            return user;
        }
    }
}