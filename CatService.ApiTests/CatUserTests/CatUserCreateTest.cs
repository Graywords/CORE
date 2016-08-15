using System;
using CatService.ApiTests.Infrastructure;
using CatService.BL.CouchDbProvider.Interfaces;
using CatService.BL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace CatService.ApiTests.CatUserTests
{
	[TestClass]
	public class CatUserCreateTest : BaseTestClass
	{
		private readonly CatUser testUser = new CatUser
		{
			Id = "test_test_test_id",
			Email = "test@email.com",
			Name = "Monster Cat",
			PasswordHash = "password hash"
		};

		[TestMethod]
		public void ShouldCreateUser()
		{
			var couchDbContextService = Kernel.Get<ICouchDbContextService>();

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
			var couchDbContextService = Kernel.Get<ICouchDbContextService>();

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
			couchDbContextService.UpdateCatUser(catUser);

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
	}
}
