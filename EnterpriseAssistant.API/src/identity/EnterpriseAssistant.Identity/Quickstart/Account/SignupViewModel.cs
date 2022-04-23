namespace EnterpriseAssistant.Identity.Quickstart.Account
{
	public class SignupViewModel : LoginInputModel
	{
        public string Name { get; set; }

		public string Position { get; set; }

        public string PasswordConfirmation { get; set; }
    }
}
