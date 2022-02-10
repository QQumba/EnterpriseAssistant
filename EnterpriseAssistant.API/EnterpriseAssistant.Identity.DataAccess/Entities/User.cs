namespace EnterpriseAssistant.Identity.DataAccess.Entities
{
	public class User
	{
		public long Id { get; set; }

		public string Login { get; set; }

		public string Name { get; set; }

		public string Salt { get; set; }

		public string Password { get; set; }

		public bool IsSoftDeleted { get; set; } = false;
	}
}