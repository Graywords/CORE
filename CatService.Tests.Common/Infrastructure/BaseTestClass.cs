using CatService.BL.CouchDbProvider;
using CatService.BL.CouchDbProvider.Interfaces;
using CatService.BL.HttpClientWrapper;
using CatService.BL.HttpClientWrapper.Interfaces;
using CatService.BL.Infrastructure.CatExtensionsTools;
using CatService.BL.Infrastructure.CatExtensionsTools.Interfaces;
using CatService.Tests.Common.ApiClient;
using CatService.Tests.Common.ApiClient.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace CatService.Tests.Common.Infrastructure
{
	[TestClass]
	public abstract class BaseTestClass
	{
		protected IKernel Kernel;

		[TestInitialize]
		public virtual void InitContainer()
		{
			this.Kernel = new StandardKernel();
			AddBindings();
		}

		[TestCleanup]
		public virtual void CleanUp()
		{
			if (this.Kernel != null)
				this.Kernel.Dispose();
		}

		private void AddBindings()
		{
			this.Kernel.Bind<ICatRestClient>().To<CatRestClient>();
			this.Kernel.Bind<ICatUserService>().To<CatUserService>();
			this.Kernel.Bind<ICatServiceTestClient>().To<CatServiceTestClient>();
			this.Kernel.Bind<ICatSupportToolsService>().To<CatSupportToolsService>();
		}
	}
}
