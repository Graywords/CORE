
namespace CatService.BL.Constants
{
	public static class CouchDbConstants
	{
		public const string ConnectionString = "http://127.0.0.1:5984/";
		public const string Uuids = "_uuids";
		public const string CatUsersDb = "cat_users";

		public static string UuidsRequest = ConnectionString + Uuids;
		public static string CatUsersDbRequest = ConnectionString + CatUsersDb + "/";
	}
}
