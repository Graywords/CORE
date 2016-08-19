using System;
using CatService.BL.CouchDbProvider.Interfaces;
using CatService.BL.Models;
using CatService.Tests.Common.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace CatService.ApiTests.CatUserTests
{
	[TestClass]
	public class CatUserTest : BaseTestClass
	{
		private readonly CatUser testUser = new CatUser
		{
			Id = "meta_id",
			Email = "test_meta@email.com",
			Name = "Meta Cat",
			PasswordHash = "password hash"
		};

		[TestMethod]
		public void ShouldCreateUser()
		{
			var couchDbContextService = Kernel.Get<ICatUserService>();

			couchDbContextService.CreateCatUser(testUser);

			var catUser = couchDbContextService.FindCatUserById(testUser.Id);

			Assert.IsNotNull(catUser);
			Assert.IsNotNull(catUser.Revision);
			Assert.AreEqual(testUser.Id, catUser.Id);
			Assert.AreEqual(testUser.Email, catUser.Email);
			Assert.AreEqual(testUser.Name, catUser.Name);
			Assert.AreEqual(testUser.PasswordHash, catUser.PasswordHash);
			Assert.AreEqual(DateTime.UtcNow.Date, catUser.CreatedOn.Date);
			couchDbContextService.DeleteCatUser(catUser);
			catUser = couchDbContextService.FindCatUserById(testUser.Id);
			Assert.IsNull(catUser);
		}

		[TestMethod]
		public void ShouldUpdateUser()
		{
			var couchDbContextService = Kernel.Get<ICatUserService>();

			couchDbContextService.CreateCatUser(testUser);

			var catUser = couchDbContextService.FindCatUserById(testUser.Id);

			Assert.IsNotNull(catUser);
			Assert.IsNotNull(catUser.Revision);
			Assert.AreEqual(testUser.Id, catUser.Id);
			Assert.AreEqual(testUser.Email, catUser.Email);
			Assert.AreEqual(testUser.Name, catUser.Name);
			Assert.AreEqual(testUser.PasswordHash, catUser.PasswordHash);
			Assert.AreEqual(DateTime.UtcNow.Date, catUser.CreatedOn.Date);

			catUser.Email = "updated@email.com";
			var expiredRevision = catUser.Revision;
			couchDbContextService.UpdateCatUser(catUser);
			Assert.IsNotNull(catUser.Revision);
			Assert.AreNotEqual(expiredRevision, catUser.Revision);

			catUser = couchDbContextService.FindCatUserById(testUser.Id);

			Assert.IsNotNull(catUser);
			Assert.IsNotNull(catUser.Revision);
			Assert.AreEqual(testUser.Id, catUser.Id);
			Assert.AreEqual("updated@email.com", catUser.Email);
			Assert.AreEqual(testUser.Name, catUser.Name);
			Assert.AreEqual(testUser.PasswordHash, catUser.PasswordHash);
			Assert.AreEqual(DateTime.UtcNow.Date, catUser.CreatedOn.Date);

			couchDbContextService.DeleteCatUser(catUser);
			catUser = couchDbContextService.FindCatUserById(testUser.Id);
			Assert.IsNull(catUser);
		}

		[TestMethod]
		public void ShouldGetUserByName()
		{
			var couchDbContextService = Kernel.Get<ICatUserService>();

			couchDbContextService.CreateCatUser(testUser);

			var catUser = couchDbContextService.FindCatUserById(testUser.Id);

			Assert.IsNotNull(catUser);
			Assert.IsNotNull(catUser.Revision);
			Assert.AreEqual(testUser.Id, catUser.Id);
			Assert.AreEqual(testUser.Email, catUser.Email);
			Assert.AreEqual(testUser.Name, catUser.Name);
			Assert.AreEqual(testUser.PasswordHash, catUser.PasswordHash);
			Assert.AreEqual(DateTime.UtcNow.Date, catUser.CreatedOn.Date);

			catUser = couchDbContextService.FindCatUserByName(testUser.Name);

			Assert.IsNotNull(catUser);
			Assert.IsNotNull(catUser.Revision);
			Assert.AreEqual(testUser.Id, catUser.Id);
			Assert.AreEqual(testUser.Email, catUser.Email);
			Assert.AreEqual(testUser.Name, catUser.Name);
			Assert.AreEqual(testUser.PasswordHash, catUser.PasswordHash);
			Assert.AreEqual(DateTime.UtcNow.Date, catUser.CreatedOn.Date);
		}

		[TestMethod]
		public void ShouldGetUserByEmail()
		{
			var couchDbContextService = Kernel.Get<ICatUserService>();

			couchDbContextService.CreateCatUser(testUser);

			var catUser = couchDbContextService.FindCatUserById(testUser.Id);

			Assert.IsNotNull(catUser);
			Assert.IsNotNull(catUser.Revision);
			Assert.AreEqual(testUser.Id, catUser.Id);
			Assert.AreEqual(testUser.Email, catUser.Email);
			Assert.AreEqual(testUser.Name, catUser.Name);
			Assert.AreEqual(testUser.PasswordHash, catUser.PasswordHash);
			Assert.AreEqual(DateTime.UtcNow.Date, catUser.CreatedOn.Date);

			catUser = couchDbContextService.FindCatUserByEmail(testUser.Email);

			Assert.IsNotNull(catUser);
			Assert.IsNotNull(catUser.Revision);
			Assert.AreEqual(testUser.Id, catUser.Id);
			Assert.AreEqual(testUser.Email, catUser.Email);
			Assert.AreEqual(testUser.Name, catUser.Name);
			Assert.AreEqual(testUser.PasswordHash, catUser.PasswordHash);
			Assert.AreEqual(DateTime.UtcNow.Date, catUser.CreatedOn.Date);
		}

		[TestCleanup]
		public override void CleanUp()
		{
			var couchDbContextService = Kernel.Get<ICatUserService>();
			var catUser = couchDbContextService.FindCatUserById(testUser.Id);
			if (catUser != null)
				couchDbContextService.DeleteCatUser(catUser);
			base.CleanUp();
		}
	}
}
