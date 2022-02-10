namespace EnterpriseAssistant.Identity.DTOs
{
	public class IdentityUserCreateDto
	{
		public string Login { get; set; }
		
		public string Password { get; set; }

		public string Name { get; set; }

		public string Position { get; set; }
	}
}
