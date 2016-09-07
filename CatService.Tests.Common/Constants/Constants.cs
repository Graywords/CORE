
namespace CatService.Tests.Common.Constants
{
	public static class Constants
	{
		public const string Endpoint = "http://api.catservice.local/";
		public const string Token = "token";
	    public const string Register = "api/Account/register";
	    public const string ChangePassword = "api/Account/ChangePassword";


        public static string RequestToken = Endpoint + Token;
	    public static string RequestRegister = Endpoint + Register;
	    public static string RequestChangePassword = Endpoint + ChangePassword;
	}
}
