namespace EnterpriseAssistant.Identity.DTOs
{
	public class UserDto
	{
		public string Login { get; set; }

		public string Name { get; set; }

		public bool IsActive { get; set; }

		public Role Role { get; set; }
	}
}
