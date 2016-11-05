using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CatService.BL.CouchDbProvider;
using CatService.BL.CouchDbProvider.Interfaces;
using CatService.BL.HttpClientWrapper;
using CatService.BL.HttpClientWrapper.Interfaces;
using CatService.Infrastructure.Interfaces;
using CatService.Models;
using Microsoft.AspNet.Identity;
using Ninject;

namespace CatService.Infrastructure
{
	public class NinjectDependencyResolver : IDependencyResolver
	{
		private readonly IKernel kernel;
		public NinjectDependencyResolver(IKernel k)
		{
			kernel = k;
			AddBindings();
		}
		public object GetService(Type serviceType)
		{
			return kernel.Get(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return kernel.GetAll(serviceType);
		}
		private void AddBindings()
		{
			kernel.Bind<IUserStore<ApplicationUser>>().To<CatUserStore>();
			kernel.Bind<ICatUserService>().To<CatUserService>();
			kernel.Bind<ICatRestClient>().To<CatRestClient>();
			kernel.Bind<ICurrentUserInformationService>().To<UserInformationService>().InSingletonScope();
		    kernel.Bind<ICatDocumentService>().To<CatDocumentService>();
		}
	}
}