using CatService.BL.Models;
using CatService.Models;

namespace CatService.Mappings
{
	public static class ApplicationUserMapping
	{
		public static CatUser Map(this ApplicationUser applicationUser)
		{
			return new CatUser
			{
				Id = applicationUser.Id,
				Name = applicationUser.UserName,
				PasswordHash = applicationUser.PasswordHash,
				Email = applicationUser.Email
			};
		}

		public static ApplicationUser Map(this CatUser catUser)
		{
			if (catUser == null)
				return null;

			return new ApplicationUser
			{
				Id = catUser.Id,
				UserName = catUser.Name,
				PasswordHash = catUser.PasswordHash,
				Email = catUser.Email
			};
		}
	}
}