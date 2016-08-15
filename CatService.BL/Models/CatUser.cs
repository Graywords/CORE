using System;

namespace CatService.BL.Models
{
	public class CatUser : BaseModel
	{
		public string Name { get; set; }

		public string Email { get; set; }

		public string PasswordHash { get; set; }

		public DateTime CreatedOn { get; set; }
	}
}
