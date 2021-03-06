﻿
using CatService.BL.HttpClientWrapper;
using CatService.Models;

namespace CatService.Tests.Common.ApiClient.Interfaces
{
	public interface ICatServiceTestClient
	{
		bool Login(string usernameOrEmail, string password);
	    bool Register(string userName, string email, string password, string passwordConfirm);
	    bool ChangePassword(string oldPassword, string newPassword, string confirmPassword);
	    bool AddDocument(string filename,string mimetype, byte[] fileData);
		bool SavePageDocument(string url);
		ApiResponse GetPdfFile(string id);
		CatDocumentViewModel[] GetDocuments();
	}
}
