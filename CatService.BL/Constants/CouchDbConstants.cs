
namespace CatService.BL.Constants
{
	public static class CouchDbConstants
	{
		//http://localhost:5984/cat_users/_design/cat_users/_view/by_name
		public const string ConnectionString = "http://127.0.0.1:5984/";
		public const string Uuids = "_uuids";
		public const string CatUsersDb = "cat_users";
		public const string Design = "_design/";
		public const string View = "/_view/";
		public const string ByName = "by_name";
		public const string ByEmail = "by_email";
		public const string SearchByKeyFormat = "{0}?key=\"{1}\"";

	    public const string CatDocumentsDb = "cat_documents";

		public static string UuidsRequest = ConnectionString + Uuids;
		public static string CatUsersDbRequest = ConnectionString + CatUsersDb + "/";
		public static string CatUsersViewRequest = CatUsersDbRequest + Design + CatUsersDb + View;
	    public static string CatDocumentDbRequest = ConnectionString + CatDocumentsDb + "/";
	}
}
