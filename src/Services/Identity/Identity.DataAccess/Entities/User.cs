using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnterpriseAssistant.Identity.DataAccess.Entities
{
	public class User
	{
		[Column("id")]
		public long Id { get; set; }

		[Column("email")]
		public string Email { get; set; }
		
		[Column("enterprise_ids")]
		public string[] EnterpriseIds { get; set; }
		
		[Column("first_name")]
		public string FirstName { get; set; }
				
		[Column("last_name")]
		public string LastName { get; set; }

		[Column("salt")]
		public string Salt { get; set; }

		[Column("password")]
		public string Password { get; set; }
		
		[Column("created_at")]
		public DateTime CreatedAt { get; set; }

		[Column("updated_at")]
		public DateTime UpdatedAt { get; set; }

		[Column("is_soft_deleted")]
		public bool IsSoftDeleted { get; set; }
	}
}