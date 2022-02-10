namespace EnterpriseAssistant.Identity.DTOs
{
	public class IdentityUserUpdateDto
	{
		public string Login { get; set; }
		
		public string Password { get; set; }

		public string Name { get; set; }

		public bool IsActive { get; set; }

		public string Position { get; set; }
	}
}
