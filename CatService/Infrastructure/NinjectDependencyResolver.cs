using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CatService.Models;
using Microsoft.AspNet.Identity;
using Ninject;

namespace CatService.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;
        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            throw new NotImplementedException();
        }
        private void AddBindings()
        {
	        kernel.Bind<IUserStore<ApplicationUser>>().To<CatUserStore>();
        }
    }
}