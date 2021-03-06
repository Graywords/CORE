﻿
namespace CatService.Tests.Common.Constants
{
	public static class Constants
	{
		public const string Endpoint = "http://api.catservice.local/";
		public const string Token = "token";
	    public const string Register = "api/Account/register";
	    public const string ChangePassword = "api/Account/ChangePassword";

		public const string GetDocument = "api/Document";
        public  const string AddDocument = "api/Document/AddDocument";
		public const string SavePageDocument = "api/Document/SaveHtml";
		public const string GetPdfFile = "api/Document/GetPdfFile";


        public static string RequestToken = Endpoint + Token;
	    public static string RequestRegister = Endpoint + Register;
	    public static string RequestChangePassword = Endpoint + ChangePassword;
	    public static string RequestAddDocument = Endpoint + AddDocument;
		public static string RequestSavePageDocument = Endpoint + SavePageDocument;
		public static string RequestGetDocument = Endpoint + GetDocument;
		public static string RequestGetPdfFile = Endpoint + GetPdfFile;
	}
}
