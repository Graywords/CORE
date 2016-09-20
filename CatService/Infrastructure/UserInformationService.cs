using CatService.Infrastructure.Interfaces;

namespace CatService.Infrastructure
{
    public class UserInformationService : ICurrentUserInformationService, ICurrentUserInformationServiceSet
    {
	    private string currentUserId;

	    public string GetUserId()
	    {
		    return currentUserId;
	    }

	    public void SetUserId(string userId)
	    {
		    currentUserId = userId;
	    }
    }
}