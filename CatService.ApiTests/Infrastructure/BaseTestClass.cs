using CatService.BL.CouchDbProvider;
using CatService.BL.CouchDbProvider.Interfaces;
using CatService.BL.HttpClientWrapper;
using CatService.BL.HttpClientWrapper.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace CatService.ApiTests.Infrastructure
{
	[TestClass]
	public abstract class BaseTestClass
	{
		protected IKernel Kernel;

		[TestInitialize]
		public virtual void InitContainer()
		{
			Kernel = new StandardKernel();
			AddBindings();
		}

		[TestCleanup]
		public virtual void CleanUp()
		{
			if (Kernel != null)
				Kernel.Dispose();
		}

		private void AddBindings()
		{
			Kernel.Bind<ICatRestClient>().To<CatRestClient>();
			Kernel.Bind<ICatUserService>().To<CatUserService>();
		}
	}
}
