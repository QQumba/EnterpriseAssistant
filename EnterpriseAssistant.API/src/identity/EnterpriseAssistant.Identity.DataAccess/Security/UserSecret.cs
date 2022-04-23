using EnterpriseAssistant.Identity.DataAccess.Entities;

namespace EnterpriseAssistant.Identity.DataAccess.Security
{
	public class UserSecret
	{
		private UserSecret(string salt, string passwordHash)
		{
			Salt = salt;
			PasswordHash = passwordHash;
		}

		public string Salt { get; }

		public string PasswordHash { get; }

		public static UserSecret Create(string password)
		{
			var salt = BCrypt.Net.BCrypt.GenerateSalt();
			var hash = BCrypt.Net.BCrypt.HashPassword(password, salt);
			return new UserSecret(salt, hash);
		}

		public static UserSecret Create(User user)
		{
			return new UserSecret(user.Salt, user.Password);
		}

		public bool Verify(string password)
		{
			var hash = BCrypt.Net.BCrypt.HashPassword(password, Salt);
			return hash.Equals(PasswordHash);
		}
	}
}
