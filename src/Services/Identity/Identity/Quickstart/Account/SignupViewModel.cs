using System.ComponentModel.DataAnnotations;

namespace EnterpriseAssistant.Identity.Quickstart.Account
{
	public class SignupViewModel : LoginInputModel
	{
		[Required]
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        [Required]
        public string PasswordConfirmation { get; set; }
    }
}
